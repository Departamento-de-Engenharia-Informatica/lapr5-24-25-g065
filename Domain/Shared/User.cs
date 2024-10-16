 namespace Domain.Shared{
    public class User{
        public int Id { get; private set; }
        public string userName { get; private set; }
        public string email { get; private set; }
        public Role role { get; private set; }

        // Construtor para criação de um novo usuário
        public User(string u, string e,Role r){
            userName = userName;
            email = email;
            role = r;
        }
    }
}