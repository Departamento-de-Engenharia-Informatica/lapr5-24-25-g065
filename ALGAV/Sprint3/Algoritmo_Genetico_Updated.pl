:-dynamic generations/1.
:-dynamic population/1.
:-dynamic prob_crossover/1.
:-dynamic prob_mutation/1.
:-dynamic target_value/1.
:-dynamic stability_generations/1.
:-dynamic time_limit/1.
:-dynamic best_solutions/1.

% task(Id,Time).
task(s1,120).
task(s2,60).
task(s3,70).
task(s4,90).
task(s5,80).

% surgeries(NSurgeries).
surgeries(5).


% parameters initialization
initialize:-
    write('Number of generations: '),read(NG),
    (retract(generations(_));true), asserta(generations(NG)),
    write('Target solution value (-1 if not used): '),read(TV),
    (retract(target_value(_));true), asserta(target_value(TV)),
    write('Stability generations (0 if not used): '),read(SG),
    (retract(stability_generations(_));true), asserta(stability_generations(SG)),
    write('Time limit in seconds: '),read(TL),
    (retract(time_limit(_));true), asserta(time_limit(TL)),
    write('Population size: '),read(PS),
    (retract(population(_));true), asserta(population(PS)),
    write('Probability of crossover (%):'), read(P1),
    PC is P1/100,
    (retract(prob_crossover(_));true), asserta(prob_crossover(PC)),
    write('Probability of mutation (%):'), read(P2),
    PM is P2/100,
    (retract(prob_mutation(_));true), asserta(prob_mutation(PM)),
    (retract(best_solutions(_));true), asserta(best_solutions([])).

generate:-
    initialize,
    generate_population(Pop),
    write('Pop='),write(Pop),nl,
    evaluate_population(Pop,PopValue),
    write('PopValue='),write(PopValue),nl,
    order_population(PopValue,PopOrd),
    get_time(StartTime),
    generate_generation(0,PopOrd,StartTime,[PopOrd]).

generate_population(Pop):-
    population(PopSize),
    surgeries(NumT),
    findall(Task,task(Task,_),TasksList),
    generate_population(PopSize,TasksList,NumT,Pop).

generate_population(0,_,_,[]):-!.
generate_population(PopSize,TasksList,NumT,[Ind|Rest]):-
    PopSize1 is PopSize-1,
    generate_population(PopSize1,TasksList,NumT,Rest),
    generate_individual(TasksList,NumT,Ind),
    not(member(Ind,Rest)).
generate_population(PopSize,TasksList,NumT,L):-
    generate_population(PopSize,TasksList,NumT,L).



generate_individual([G],1,[G]):-!.

generate_individual(TasksList,NumT,[G|Rest]):-
    NumTemp is NumT + 1, % to use with random
    random(1,NumTemp,N),
    remove(N,TasksList,G,NewList),
    NumT1 is NumT-1,
    generate_individual(NewList,NumT1,Rest).

remove(1,[G|Rest],G,Rest).
remove(N,[G1|Rest],G,[G1|Rest1]):- N1 is N-1,
            remove(N1,Rest,G,Rest1).


evaluate_population([],[]).
evaluate_population([Ind|Rest],[Ind*V|Rest1]):-
    evaluate(Ind,V),
    evaluate_population(Rest,Rest1).

evaluate(Seq,V):- evaluate(Seq,0,V).

% New evaluation
evaluate([], CurrentTime, CurrentTime).
evaluate([T|Rest], Inst, V) :-
    task(T, Dur),
    NewInst is Inst + Dur,
    evaluate(Rest, NewInst, V).

order_population(PopValue,PopValueOrd):-
    bsort(PopValue,PopValueOrd).

bsort([X],[X]):-!.
bsort([X|Xs],Ys):-
    bsort(Xs,Zs),
    bchange([X|Zs],Ys).


bchange([X],[X]):-!.

bchange([X*VX,Y*VY|L1],[Y*VY|L2]):-
    VX>VY,!,
    bchange([X*VX|L1],L2).

bchange([X|L1],[X|L2]):-bchange(L1,L2).



generate_generation(N,Pop,StartTime,History):-
    get_time(CurrentTime),
    time_limit(TL),
    ElapsedTime is CurrentTime - StartTime,
    (ElapsedTime > TL ->
        write('Time limit reached. '),
        present_best_solution(Pop)
    ;
        (check_termination(N,Pop,History) ->
            present_best_solution(Pop)
        ;
            write('Generation '), write(N), write(':'), nl,
            write(Pop), nl,
            random_permutation(Pop, ShuffledPop),
            crossover(ShuffledPop,NPop1),
            mutation(NPop1,NPop),
            evaluate_population(NPop,NPopValue),
            select_next_generation(Pop,NPopValue,NextGen),
            N1 is N+1,
            update_history(History,NextGen,NewHistory),
            generate_generation(N1,NextGen,StartTime,NewHistory)
        )
    ).


check_termination(N,Pop,History):-
    generations(NG),
    (NG > 0, N >= NG);
    check_target_value(Pop);
    check_stability(History).


check_target_value([Ind*Value|_]):-
    target_value(TV),
    TV > 0,
    Value =< TV.

check_stability(History):-
    stability_generations(SG),
    SG > 0,
    length(History, HL),
    HL >= SG,
    take(SG, History, RecentGens),
    all_equal(RecentGens).

take(N,List,Taken):-
    length(Taken,N),
    append(Taken,_,List).

all_equal([]).
all_equal([_]).
all_equal([X,X|T]):-all_equal([X|T]).

update_history(History,NewGen,NewHistory):-
    stability_generations(SG),
    (SG > 0 ->
        append([NewGen],History,TempHistory),
        (length(TempHistory,L), L > SG ->
            take(SG,TempHistory,NewHistory)
        ;
            NewHistory = TempHistory
        )
    ;
        NewHistory = History
    ).

present_best_solution([Best|_]):-
    write('Best solution found: '), write(Best), nl.


merge_populations(Pop1, Pop2, MergedPop) :-
    append(Pop1, Pop2, TotalPop),
    remove_duplicates(TotalPop, MergedPop).

remove_duplicates([], []).
remove_duplicates([X*V|Rest], [X*V|FilteredRest]) :-
    remove_individual(X, Rest, TempRest),
    remove_duplicates(TempRest, FilteredRest).

remove_individual(_, [], []).
remove_individual(Ind, [X*_|Rest], FilteredRest) :-
    Ind == X, !,
    remove_individual(Ind, Rest, FilteredRest).
remove_individual(Ind, [X*V|Rest], [X*V|FilteredRest]) :-
    remove_individual(Ind, Rest, FilteredRest).


select_top_individuals(SortedPop, P, TopP, Remaining) :-
    length(SortedPop, TotalLen),
    (P > TotalLen ->
        TopP = SortedPop,
        Remaining = []
    ;
        split_list(SortedPop, P, TopP, Remaining)
    ).

split_list(List, N, First, Rest) :-
    length(First, N),
    append(First, Rest, List).

process_remaining([], []).
process_remaining([Ind*Val|Rest], [Ind*Val*RandVal|ProcessedRest]) :-
    random(0.0, 1.0, Rand),
    RandVal is Val * Rand,
    process_remaining(Rest, ProcessedRest).

sort_by_random_weight(List, Sorted) :-
    bsort_random_weight(List, Sorted).

bsort_random_weight([X], [X]) :- !.
bsort_random_weight([X|Xs], Ys) :-
    bsort_random_weight(Xs, Zs),
    bchange_random_weight([X|Zs], Ys).

bchange_random_weight([X], [X]) :- !.
bchange_random_weight([Ind1*Val1*RandVal1, Ind2*Val2*RandVal2|Rest],
                     [Ind2*Val2*RandVal2|ResultRest]) :-
    RandVal1 > RandVal2, !,
    bchange_random_weight([Ind1*Val1*RandVal1|Rest], ResultRest).
bchange_random_weight([X|Rest], [X|ResultRest]) :-
    bchange_random_weight(Rest, ResultRest).

select_next_generation(CurrentPop, NewPop, FinalPop) :-
    population(N),
    P is round(N * 0.2),

    merge_populations(CurrentPop, NewPop, MergedPop),

    order_population(MergedPop, SortedPop),

    select_top_individuals(SortedPop, P, TopP, Remaining),

    process_remaining(Remaining, ProcessedRemaining),

    sort_by_random_weight(ProcessedRemaining, SortedRemaining),

    RemainderNeeded is N - P,
    select_top_individuals(SortedRemaining, RemainderNeeded, SelectedRemainder, _),

    strip_random_weights(SelectedRemainder, StrippedRemainder),
    append(TopP, StrippedRemainder, FinalPop).


strip_random_weights([], []).
strip_random_weights([Ind*Val*_|Rest], [Ind*Val|StrippedRest]) :-
    strip_random_weights(Rest, StrippedRest).



generate_crossover_points(P1,P2):- generate_crossover_points1(P1,P2).

generate_crossover_points1(P1,P2):-
	surgeries(N),
	NTemp is N+1,
	random(1,NTemp,P11),
	random(1,NTemp,P21),
	P11\==P21,!,
	((P11<P21,!,P1=P11,P2=P21);P1=P21,P2=P11).
generate_crossover_points1(P1,P2):-
	generate_crossover_points1(P1,P2).


crossover([], []).
crossover([Ind*_], [Ind]).
crossover(Pop, NewPop) :-
    shuffle_population(Pop, ShuffledPop),
    crossover_pairs(ShuffledPop, NewPop).


shuffle_population(Pop, ShuffledPop) :-
    random_permutation(Pop, ShuffledPop).


crossover_pairs([], []).
crossover_pairs([Ind1*_, Ind2*_|Rest], [NInd1, NInd2|Rest1]) :-
    generate_crossover_points(P1, P2),
    prob_crossover(Pcruz),
    random(0.0, 1.0, Pc),
    ((Pc =< Pcruz, !,
        cross(Ind1, Ind2, P1, P2, NInd1),
        cross(Ind2, Ind1, P1, P2, NInd2))
    ;
    (NInd1 = Ind1, NInd2 = Ind2)),
    crossover_pairs(Rest, Rest1).
crossover_pairs([Ind*_|Rest], [Ind|Rest1]) :-
    crossover_pairs(Rest, Rest1).


fillh([ ],[ ]).

fillh([_|R1],[h|R2]):-
	fillh(R1,R2).

sublist(L1,I1,I2,L):-I1 < I2,!,
    sublist1(L1,I1,I2,L).

sublist(L1,I1,I2,L):-sublist1(L1,I2,I1,L).

sublist1([X|R1],1,1,[X|H]):-!, fillh(R1,H).

sublist1([X|R1],1,N2,[X|R2]):-!,N3 is N2 - 1,
	sublist1(R1,1,N3,R2).

sublist1([_|R1],N1,N2,[h|R2]):-N3 is N1 - 1,
		N4 is N2 - 1,
		sublist1(R1,N3,N4,R2).

rotate_right(L,K,L1):- surgeries(N),
	T is N - K,
	rr(T,L,L1).

rr(0,L,L):-!.

rr(N,[X|R],R2):- N1 is N - 1,
	append(R,[X],R1),
	rr(N1,R1,R2).

remove([],_,[]):-!.

remove([X|R1],L,[X|R2]):- not(member(X,L)),!,
        remove(R1,L,R2).

remove([_|R1],L,R2):-
    remove(R1,L,R2).

insert([],L,_,L):-!.
insert([X|R],L,N,L2):-
    surgeries(T),
    ((N>T,!,N1 is N mod T);N1 = N),
    insert1(X,N1,L,L1),
    N2 is N + 1,
    insert(R,L1,N2,L2).


insert1(X,1,L,[X|L]):-!.
insert1(X,N,[Y|L],[Y|L1]):-
    N1 is N-1,
    insert1(X,N1,L,L1).

cross(Ind1,Ind2,P1,P2,NInd11):-
    sublist(Ind1,P1,P2,Sub1),
    surgeries(NumT),
    R is NumT-P2,
    rotate_right(Ind2,R,Ind21),
    remove(Ind21,Sub1,Sub2),
    P3 is P2 + 1,
    insert(Sub2,Sub1,P3,NInd1),
    removeh(NInd1,NInd11).


removeh([],[]).

removeh([h|R1],R2):-!,
    removeh(R1,R2).

removeh([X|R1],[X|R2]):-
    removeh(R1,R2).

mutation([],[]).
mutation([Ind|Rest],[NInd|Rest1]):-
	prob_mutation(Pmut),
	random(0.0,1.0,Pm),
	((Pm < Pmut,!,mutacao1(Ind,NInd));NInd = Ind),
	mutation(Rest,Rest1).

mutacao1(Ind,NInd):-
	generate_crossover_points(P1,P2),
	mutacao22(Ind,P1,P2,NInd).

mutacao22([G1|Ind],1,P2,[G2|NInd]):-
	!, P21 is P2-1,
	mutacao23(G1,P21,Ind,G2,NInd).
mutacao22([G|Ind],P1,P2,[G|NInd]):-
	P11 is P1-1, P21 is P2-1,
	mutacao22(Ind,P11,P21,NInd).

mutacao23(G1,1,[G2|Ind],G2,[G1|Ind]):-!.
mutacao23(G1,P,[G|Ind],G2,[G|NInd]):-
	P1 is P-1,
	mutacao23(G1,P1,Ind,G2,NInd).
