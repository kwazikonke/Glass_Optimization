using GlassOpt.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GlassOpt.Services
{
    public class GlassCuttingOptimizerService
    {
        public List<StockSheet> OptimizingCutting(string filePath)
        {
            var panels = new List<Panel>();
            var stockSheets = new List<StockSheet>();

            try
            {
                var lines = File.ReadAllLines(filePath);

                var section = "";
                foreach (var item in lines)
                {
                    if (item == "[Sources]")
                    {
                        section = "Sources";
                        continue;
                    }
                    else if (item == "[Parts]")
                    {
                        section = "Parts";
                        continue;
                    }
                    if (!string.IsNullOrEmpty(item.Trim()))
                    {
                        var data = item.Split(',');

                        if (section == "Sources")
                        {
                            var stockSheet = new StockSheet
                            {
                                StockSheet_Id = int.Parse(data[0]),
                                Width = int.Parse(data[1]),
                                Height = int.Parse(data[2]),
                                Qty = int.Parse(data[3]),
                                Cost = decimal.Parse(data[4].TrimStart('R').Replace(" ", "").Replace(",", "")),
                                AllocatedPanels = new List<Panel>() 
                            };
                            stockSheets.Add(stockSheet);
                        }
                        else if (section == "Parts")
                        {
                            var panel = new Panel
                            {
                                Panel_Id = int.Parse(data[0]),
                                Width = int.Parse(data[1]),
                                Height = int.Parse(data[2]),
                                Qty = int.Parse(data[3]),
                                AllocatedStockSheet = null
                            };
                            panels.Add(panel);
                        }
                    }
                }
                // Sorting PANELS BY Descending order in FFD
                panels = panels.OrderByDescending(p => p.Width * p.Height).ToList();

                // Applying FFD 
                foreach (var panel in panels)
                {
                    bool allocated = false;
                    foreach (var sheet in stockSheets)
                    {
                        if (panel.Width <= sheet.Width && panel.Height <= sheet.Height && sheet.Qty > 0)
                        {
                            panel.AllocatedStockSheet = sheet;
                            sheet.AllocatedPanels.Add(panel);
                            sheet.Qty--;
                            allocated = true;
                            break;
                        }
                    }
                    if (!allocated)
                    {
                        var newSheet = new StockSheet
                        {
                            StockSheet_Id = stockSheets.Count + 1,
                            Width = panel.Width,
                            Height = panel.Height,
                            Qty = 1,
                            Cost = 0,
                            AllocatedPanels = new List<Panel>() { panel } 
                        };
                        stockSheets.Add(newSheet);
                        panel.AllocatedStockSheet = newSheet;
                    }
                }
                //stockSheets = stockSheets.GroupBy(s => new { s.Width, s.Height })
                //                         .Select(g => g.First())
                //                         .ToList();

                return stockSheets;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading or processing file: {ex.Message}");
                return new List<StockSheet>(); 
            }
        }
    }
}
