using Grasshopper;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Attributes;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TableDataInput
{
    public class TableDataInputComp : GH_Component
    {
        private GH_Structure<GH_String> _data;
        public TableDataInputComp() : base("Table Data Input", "TDI", "Table Data Input", "Params", "Input")
        {
            _data = new GH_Structure<GH_String>();
        }
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
        }
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("Output", "O", "Output", GH_ParamAccess.tree);
        }
        protected override void SolveInstance(IGH_DataAccess da)
        {
            if (_data.IsEmpty)
                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "Table Data Input failed to collect data");
            da.SetDataTree(0, _data);
        }
        public override void CreateAttributes() => m_attributes = new TableDataInputAttributes(this);
        public void DisplayEditor()
        {
            var editor = new TableEditor();
            GH_WindowsFormUtil.CenterFormOnCursor(editor, true);
            editor.Data = _data;
            if (editor.ShowDialog(Instances.DocumentEditor) != DialogResult.OK)
                return;
            _data.Clear();
            _data = editor.Data;
            ExpireSolution(true);
        }
        public override bool Write(GH_IO.Serialization.GH_IWriter writer)
        {
            _data.Write(writer.CreateChunk("TableData"));
            return base.Write(writer);
        }
        public override bool Read(GH_IO.Serialization.GH_IReader reader)
        {
            _data.Read(reader.FindChunk("TableData"));
            return base.Read(reader);
        }
        protected override Bitmap Icon => Properties.Resources.icon;
        public override Guid ComponentGuid => new Guid("03CC69EA-8864-454E-8EBB-DBD2D3ED2504");
    }
    public class TableDataInputAttributes : GH_ComponentAttributes
    {
        public TableDataInputAttributes(IGH_Component owner) : base(owner) { }
        public override GH_ObjectResponse RespondToMouseDoubleClick(GH_Canvas sender, GH_CanvasMouseEvent e)
        {
            if (e.Button != MouseButtons.Left || !Bounds.Contains(e.CanvasLocation))
                return base.RespondToMouseDoubleClick(sender, e);
            ((TableDataInputComp)Owner).DisplayEditor();
            return GH_ObjectResponse.Handled;
        }
    }
}