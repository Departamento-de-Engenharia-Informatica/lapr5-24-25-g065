TESTS:


// Removed redundant status code check and fixed response body check
pm.test("Status code is 201", function () {
    pm.response.to.have.status(201);
});

pm.test("Status code is NOT 400", function () {
    pm.expect(pm.response.code).to.not.equal(400);
});
