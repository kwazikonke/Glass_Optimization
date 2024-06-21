using System.ComponentModel.DataAnnotations;

namespace GlassOpt.Models
{
    public class StockSheet
    {
        [Key]
        public int StockSheet_Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Qty { get; set; }
        public decimal Cost { get; set; }
        public List<Panel> AllocatedPanels { get; set; } = new List<Panel>();
    }
}
