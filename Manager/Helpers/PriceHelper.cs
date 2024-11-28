namespace Manager.Helpers
{
    public static class PriceHelper
    {
        /// <summary>
        /// Định dạng giá thành chuỗi có dấu chấm (phân cách hàng nghìn).
        /// </summary>
        /// <param name="price">Giá tiền (kiểu decimal).</param>
        /// <returns>Chuỗi định dạng giá, ví dụ: 100000 -> "100.000".</returns>
        public static string FormatPrice(decimal price)
        {
            return string.Format("{0:N0}", price).Replace(",", ".");
        }
    }
}
