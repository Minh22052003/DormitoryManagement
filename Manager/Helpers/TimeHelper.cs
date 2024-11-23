namespace Manager.Helpers
{
    public class TimeHelper
    {
        public static string TimeAgo(DateTime? creationDate)
        {
            if (creationDate == null) return "Không xác định";
            var timeSpan = DateTime.Now - creationDate.Value;

            if (timeSpan.TotalSeconds < 60)
                return $"{timeSpan.Seconds} giây trước";
            if (timeSpan.TotalMinutes < 60)
                return $"{timeSpan.Minutes} phút trước";
            if (timeSpan.TotalHours < 24)
                return $"{timeSpan.Hours} giờ trước";
            if (timeSpan.TotalDays < 30)
                return $"{timeSpan.Days} ngày trước";
            if (timeSpan.TotalDays < 365)
                return $"{Math.Floor(timeSpan.TotalDays / 30)} tháng trước";

            return $"{Math.Floor(timeSpan.TotalDays / 365)} năm trước";
        }
    }
}
