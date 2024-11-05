namespace MySocialApp.Domain;

public class UserFollow : Entity
{
    public string FollowingUserId { get; set; }
    public User FollowingUser { get; set; }
    public string FollowerUserId { get; set; }
    public User FollowerUser { get; set; }
}