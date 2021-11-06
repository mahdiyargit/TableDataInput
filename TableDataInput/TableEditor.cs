using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace TableDataInput
{
    public class TableEditor : Form
    {
        private Button _btnCancel;
        private Button _btnOk;
        private Panel _pnlButtons;
        private Panel _panel;
        private ReoGridControl _grid;

        public TableEditor()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this._btnCancel = new System.Windows.Forms.Button();
            this._btnOk = new System.Windows.Forms.Button();
            this._pnlButtons = new System.Windows.Forms.Panel();
            this._panel = new System.Windows.Forms.Panel();
            this._grid = new unvell.ReoGrid.ReoGridControl();
            this._pnlButtons.SuspendLayout();
            this._panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _btnCancel
            // 
            this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this._btnCancel.Location = new System.Drawing.Point(538, 8);
            this._btnCancel.Name = "_btnCancel";
            this._btnCancel.Size = new System.Drawing.Size(80, 24);
            this._btnCancel.TabIndex = 3;
            this._btnCancel.Text = "Cancel";
            this._btnCancel.UseVisualStyleBackColor = true;
            // 
            // _btnOk
            // 
            this._btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._btnOk.Dock = System.Windows.Forms.DockStyle.Right;
            this._btnOk.Location = new System.Drawing.Point(458, 8);
            this._btnOk.Name = "_btnOk";
            this._btnOk.Size = new System.Drawing.Size(80, 24);
            this._btnOk.TabIndex = 2;
            this._btnOk.Text = "OK";
            this._btnOk.UseVisualStyleBackColor = true;
            // 
            // _pnlButtons
            // 
            this._pnlButtons.Controls.Add(this._btnOk);
            this._pnlButtons.Controls.Add(this._btnCancel);
            this._pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._pnlButtons.Location = new System.Drawing.Point(3, 399);
            this._pnlButtons.Name = "_pnlButtons";
            this._pnlButtons.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this._pnlButtons.Size = new System.Drawing.Size(618, 32);
            this._pnlButtons.TabIndex = 1;
            // 
            // _panel
            // 
            this._panel.Controls.Add(this._grid);
            this._panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel.Location = new System.Drawing.Point(3, 3);
            this._panel.Name = "_panel";
            this._panel.Size = new System.Drawing.Size(618, 396);
            this._panel.TabIndex = 1;
            // 
            // _grid
            // 
            this._grid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._grid.ColumnHeaderContextMenuStrip = null;
            this._grid.Cursor = System.Windows.Forms.Cursors.Default;
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.LeadHeaderContextMenuStrip = null;
            this._grid.Location = new System.Drawing.Point(0, 0);
            this._grid.Name = "_grid";
            this._grid.RowHeaderContextMenuStrip = null;
            this._grid.Script = null;
            this._grid.SheetTabContextMenuStrip = null;
            this._grid.SheetTabNewButtonVisible = true;
            this._grid.SheetTabVisible = false;
            this._grid.SheetTabWidth = 400;
            this._grid.ShowScrollEndSpacing = true;
            this._grid.Size = new System.Drawing.Size(618, 396);
            this._grid.TabIndex = 0;
            this._grid.Text = "grid";
            // 
            // TableEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 434);
            this.Controls.Add(this._panel);
            this.Controls.Add(this._pnlButtons);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TableEditor";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Table Editor";
            this._pnlButtons.ResumeLayout(false);
            this._panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        public GH_Structure<GH_String> Data
        {
            get
            {
                var tree = new GH_Structure<GH_String>();
                _grid.Worksheets[0].IterateCells(RangePosition.EntireRange, true, (row, col, cell) =>
                {
                    if (cell.Data != null)
                        tree.Append(new GH_String(cell.Data.ToString()), new GH_Path(row, col));
                    return true;
                });
                return tree;
            }
            set
            {
                _grid.Worksheets[0].SelectionForwardDirection = SelectionForwardDirection.Down;
                foreach (var path in value.Paths)
                    _grid.Worksheets[0][path.Indices[0], path.Indices[1]] = value[path][0].Value;
            }
        }
    }
}
