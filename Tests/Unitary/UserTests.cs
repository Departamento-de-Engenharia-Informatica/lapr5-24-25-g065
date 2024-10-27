using DDDSample1.Domain.Passwords;
using DDDSample1.Domain.Users;
using System;
using Xunit;

public class UserTest
{
    [Theory]
    [InlineData("ericcartman@gmail.com", "cartman445", "Patient", "StrongPassword123!")]
    [InlineData("stanmarsh@gmail.com", "stan", "Doctor", "SecurePass456@")]
    [InlineData("kylebrofloski@gmail.com", "kyleb", "Nurse", "Password789#")]
    public void WhenPassingCorrectData_NewUserIsCreated(string email, string username, string role, string passwordString)
    {
        // Create Password instance
        var password = new Password(passwordString);
        var userRole = Enum.Parse<Role>(role);
        var user = new User(username,email,  userRole, password); // Pass Password object

        Assert.Equal(email, user.Email);
        Assert.Equal(username, user.UserName);
        Assert.Equal(userRole, user.Role);
        Assert.Equal(passwordString, user.Password.Pass); // Assert password
    }

    [Theory]
    [InlineData("ericcartmangmail.com", "cartman445", "Patient", "password123")]
    [InlineData("ericcartman.com", "cartman445", "Patient", "password123")]
    [InlineData("ericcartmangmail", "cartman445", "Patient", "password123")]
    public void WhenPassingInvalidEmail_ExceptionIsThrown(string email, string username, string role, string passwordString)
    {
        // Create Password instance
        var password = new Password(passwordString);
        Assert.Throws<ArgumentException>(() =>
        {
            var userRole = Enum.Parse<Role>(role);
            _ = new User(username,email,  userRole, password); // Pass Password object
        });
    }

    [Theory]
    [InlineData("stanmarsh@gmail.com", "st an", "Doctor", "StrongPass456@")]
    [InlineData("stanmarsh@gmail.com", " stan", "Doctor", "SecurePass456@")]
    [InlineData("stanmarsh@gmail.com", "stan ", "Doctor", "Password789#")]
    public void WhenPassingInvalidUsername_ExceptionIsThrown(string email, string username, string role, string passwordString)
    {
        // Create Password instance
        var password = new Password(passwordString);
        Assert.Throws<ArgumentException>(() =>
        {
            var userRole = Enum.Parse<Role>(role);
            _ = new User(username,email,  userRole, password); // Pass Password object
        });
    }

    [Theory]
    [InlineData("kylebrofloski@gmail.com", "kyleb", "Pilot", "StrongPassword123!")]
    [InlineData("kylebrofloski@gmail.com", "kyleb", "Clown", "SecurePass456@")]
    [InlineData("kylebrofloski@gmail.com", "kyleb", "Police", "Password789#")]
    public void WhenPassingInvalidRole_ExceptionIsThrown(string email, string username, string role, string passwordString)
    {
        // Create Password instance
        var password = new Password(passwordString);
        Assert.Throws<ArgumentException>(() =>
        {
            // Attempt to parse role from enum will fail and throw exception before constructing the user
         var userRole = Enum.Parse<Role>(role, ignoreCase: true);
            _ = new User(username,email,  userRole, password); // Pass Password object
        });
    }

    [Theory]
    [InlineData("ericcartmangmail.com", "cartman 445", "Loony", "StrongPassword123!")]
    [InlineData("stanmarsh.com", "s tan", "Medic", "SecurePass456@")]
    [InlineData("kylebrofloski", "kyle b", "Assistant", "Password789#")]
    public void WhenPassingInvalidParams_ExceptionIsThrown(string email, string username, string role, string passwordString)
    {
        // Create Password instance
        var password = new Password(passwordString);
        Assert.Throws<ArgumentException>(() =>
        {
            var userRole = Enum.Parse<Role>(role, ignoreCase: true);
            _ = new User(username,email,  userRole, password); // Pass Password object
        });
    }
}
