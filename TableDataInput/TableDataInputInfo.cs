using Grasshopper.Kernel;
using System;
using System.Drawing;
namespace TableDataInput
{
    public class TableDataInputInfo : GH_AssemblyInfo
    {
        public override string Name => "TableDataInput";
        public override Bitmap Icon => Properties.Resources.icon;
        public override string Description => "";
        public override Guid Id => new Guid("ACC5DB30-0FA4-40A2-904B-397964568CC0");
        public override string AuthorName => "Mahdiyar";
        public override string AuthorContact => "info@Mahdiyar.IO";
    }
}