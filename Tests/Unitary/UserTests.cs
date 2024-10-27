/*using DDDSample1.Domain.Users;
using System;
using Xunit;

public class UserTest
{
    [Theory]
    [InlineData("ericcartman@gmail.com", "cartman445", "Patient")]
    [InlineData("stanmarsh@gmail.com", "stan", "Doctor")]
    [InlineData("kylebrofloski@gmail.com", "kyleb", "Nurse")]
    public void WhenPassingCorrectData_NewUserIsCreated(string email, string username, string role)
    {
        var userRole = Enum.Parse<Role>(role);
        var user = new User(email, username, userRole);

        Assert.Equal(email, user.Email);
        Assert.Equal(username, user.UserName);
        Assert.Equal(userRole, user.Role);
    }

    [Theory]
    [InlineData("ericcartmangmail.com", "cartman445", "Patient")]
    [InlineData("ericcartman.com", "cartman445", "Patient")]
    [InlineData("ericcartmangmail", "cartman445", "Patient")]
    public void WhenPassingInvalidEmail_ExceptionIsThrown(string email, string username, string role)
    {
        var userRole = Enum.Parse<Role>(role);
        Assert.Throws<ArgumentException>(() =>
            _ = new User(email, username, userRole)
        );
    }

    [Theory]
    [InlineData("stanmarsh@gmail.com", "st an", "Doctor")]
    [InlineData("stanmarsh@gmail.com", " stan", "Doctor")]
    [InlineData("stanmarsh@gmail.com", "stan ", "Doctor")]
    public void WhenPassingInvalidUsername_ExceptionIsThrown(string email, string username, string role)
    {
        var userRole = Enum.Parse<Role>(role);
        Assert.Throws<ArgumentException>(() =>
            _ = new User(email, username, userRole)
        );
    }

    [Theory]
    [InlineData("kylebrofloski@gmail.com", "kyleb", "Pilot")]
    [InlineData("kylebrofloski@gmail.com", "kyleb", "Clown")]
    [InlineData("kylebrofloski@gmail.com", "kyleb", "Police")]
    public void WhenPassingInvalidRole_ExceptionIsThrown(string email, string username, string role)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            // Attempt to parse role from enum will fail and throw exception before constructing the user
            var userRole = Enum.Parse<Role>(role, ignoreCase: true);
            _ = new User(email, username, userRole);
        });
    }

    [Theory]
    [InlineData("ericcartmangmail.com", "cartman 445", "Loony")]
    [InlineData("stanmarsh.com", "s tan", "Medic")]
    [InlineData("kylebrofloski", "kyle b", "Assistant")]
    public void WhenPassingInvalidParams_ExceptionIsThrown(string email, string username, string role)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var userRole = Enum.Parse<Role>(role, ignoreCase: true);
            _ = new User(email, username, userRole);
        });
    }
}
*/