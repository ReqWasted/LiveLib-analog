namespace LiveLib.Application.Features.Users.UpdateUser
{
	public class UserUpdateDto
	{
		public string Username { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string Role { get; set; } = null!;
	}
}
