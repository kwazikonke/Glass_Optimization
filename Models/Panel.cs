using System.ComponentModel.DataAnnotations;

namespace GlassOpt.Models
{
    public class Panel
    {
        [Key]
        public int Panel_Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Qty { get; set; }

        // Reference to the stock sheet this panel is allocated to
        public StockSheet AllocatedStockSheet { get; set; }
    }
}
