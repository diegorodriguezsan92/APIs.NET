namespace UniversityAPIrestfull.Models.DataModels
{
    public class JwtSettings
    {
        public bool ValidateIsuserSigninKey { get; set; }
        public string IsuserSigninKey { set; get; } = String.Empty;

        public bool ValidateIsuser { get; set; } = true;
        public string? ValidIsuser { get; set; }

        public bool ValidateAudience { get; set; } = true;
        public string? ValidAudience { get; set; }

        public bool RequireExpirationTime { get; set; } = true;
        public bool ValidateLifetime { get; set; } = true;
    }
}
