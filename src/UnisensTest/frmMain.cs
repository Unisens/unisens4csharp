using System;
using System.Drawing;
using System.Windows.Forms;
using org.unisens;

namespace UnisensTest
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            folderDialogue.SelectedPath = "D:\\projects\\Java2Net\\Example_002";
            txtPath.Text = folderDialogue.SelectedPath;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            var result = folderDialogue.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPath.Text = folderDialogue.SelectedPath.ToUpper();
            }
        }

        private void ShowMessage(string msg)
        {
            lblMsg.ForeColor = Color.Black;
            lblMsg.Text = msg;
        }

        private void ShowInfo(string info)
        {
            lblMsg.ForeColor = Color.Green;
            lblMsg.Text = info;
        }

        private void ShowError(string error)
        {
            lblMsg.ForeColor = Color.Red;
            lblMsg.Text = error;
        }

        private void btnSbin_Click(object sender, EventArgs e)
        {
            org.unisens.Unisens u = null;
            try
            {
                UnisensFactory uf = UnisensFactoryBuilder.createFactory();

                u = uf.createUnisens(folderDialogue.SelectedPath);
                SignalEntry se = u.createSignalEntry("signal.bin", new String[] { "A", "B" }, DataType.INT16, 250);
                var A = new short[][] { new short[] { 1, 4 }, new short[] { 2, 5 }, new short[] { 3, 6 } };
                //se.setFileFormat(se.createXmlFileFormat());
                var B = new short[][] { new short[] { 2, 4 }, new short[] { 3, 5 }, new short[] { 4, 6 } };
                se.append(A);
                se.append(B);
                u.save();
                ShowInfo("Parse file {signal.bin} suceed!");

            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            finally
            {
                if (u != null)
                    u.closeAll();
            }
        }

        private void btnScsv_Click(object sender, EventArgs e)
        {
            org.unisens.Unisens u = null;
            try
            {
                UnisensFactory uf = UnisensFactoryBuilder.createFactory();

                u = uf.createUnisens(folderDialogue.SelectedPath);
                var se = u.createSignalEntry("signal.csv", new String[] { "A", "B" }, DataType.INT16, 250);
                var A = new short[][] { new short[] { 1, 4 }, new short[] { 2, 5 }, new short[] { 3, 6 } };
                CsvFileFormat cff = se.createCsvFileFormat();
                cff.setSeparator(";");
                cff.setDecimalSeparator(".");
                se.setFileFormat(cff);
                se.append(A);
                u.save();
                ShowInfo("Parse file {signal.csv} suceed!");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            finally
            {
                if (u != null)
                    u.closeAll();
            }
        }

        private void btnSxml_Click(object sender, EventArgs e)
        {
            org.unisens.Unisens u = null;
            try
            {
                UnisensFactory uf = UnisensFactoryBuilder.createFactory();

                u = uf.createUnisens(folderDialogue.SelectedPath);
                var se = u.createSignalEntry("signal.xml", new String[] { "A", "B" }, DataType.INT16, 250);
                var A = new short[][] { new short[] { 1, 4 }, new short[] { 2, 5 }, new short[] { 3, 6 } };
                se.setFileFormat(se.createXmlFileFormat());
                se.append(A);
                u.save();
                ShowInfo("Parse file {signal.xml} suceed!");

            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            finally
            {
                if (u != null)
                    u.closeAll();
            }
        }

        private void btnEbin_Click(object sender, EventArgs e)
        {
            org.unisens.Unisens u = null;
            try
            {
                UnisensFactory uf = UnisensFactoryBuilder.createFactory();

                u = uf.createUnisens(folderDialogue.SelectedPath);
                var ee = u.createEventEntry("event.bin", 1000);
                ee.setFileFormat(ee.createBinFileFormat());
                ee.setCommentLength(6);
                ee.setTypeLength(1);
                ee.append(new Event(124, "N", "NORMAL"));
                ee.append(new Event(346, "N", "NORMAL"));
                ee.append(new Event(523, "V", "PVC"));
                u.save();
                ShowInfo("Parse file {event.bin} suceed!");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            finally
            {
                if (u != null)
                    u.closeAll();
            }
        }

        private void btnEcsv_Click(object sender, EventArgs e)
        {
            org.unisens.Unisens u = null;
            try
            {
                UnisensFactory uf = UnisensFactoryBuilder.createFactory();

                u = uf.createUnisens(folderDialogue.SelectedPath);
                EventEntry ee = u.createEventEntry("event.csv", 1000);
                var cff = ee.createCsvFileFormat();
                cff.setSeparator(";");
                cff.setDecimalSeparator(".");
                ee.setFileFormat(cff);
                ee.append(new Event(124, "N", "NORMAL"));
                ee.append(new Event(346, "N", "NORMAL"));
                ee.append(new Event(523, "V", "PVC"));
                u.save();
                ShowInfo("Parse file {event.csv} suceed!");

            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            finally
            {
                if (u != null)
                    u.closeAll();
            }
        }

        private void btnExml_Click(object sender, EventArgs e)
        {
            org.unisens.Unisens u = null;
            try
            {
                UnisensFactory uf = UnisensFactoryBuilder.createFactory();

                u = uf.createUnisens(folderDialogue.SelectedPath);
                var ee = u.createEventEntry("event.xml", 1000);
                ee.setFileFormat(ee.createXmlFileFormat());
                ee.append(new Event(124, "N", "NORMAL"));
                ee.append(new Event(346, "N", "NORMAL"));
                ee.append(new Event(523, "V", "PVC"));
                u.save();
                ShowInfo("Parse file {event.xml} suceed!");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            finally
            {
                if (u != null)
                    u.closeAll();
            }
        }

        private void btnVbin_Click(object sender, EventArgs e)
        {
            org.unisens.Unisens u = null;
            try
            {
                UnisensFactory uf = UnisensFactoryBuilder.createFactory();

                u = uf.createUnisens(folderDialogue.SelectedPath);
                var ve = u.createValuesEntry("values.bin", new String[] { "A", "B" }, DataType.INT16, 250);
                ve.setFileFormat(ve.createBinFileFormat());
                ve.append(new Value(1320, new short[] { 1, 4 }));
                ve.append(new Value(22968, new short[] { 2, 5 }));
                ve.append(new Value(30232, new short[] { 3, 6 }));
                u.save();
                ShowInfo("Parse file {values.bin} suceed!");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            finally
            {
                if (u != null)
                    u.closeAll();
            }
        }

        private void btnVcsv_Click(object sender, EventArgs e)
        {
            org.unisens.Unisens u = null;
            try
            {
                UnisensFactory uf = UnisensFactoryBuilder.createFactory();

                u = uf.createUnisens(folderDialogue.SelectedPath);
                ValuesEntry ve = u.createValuesEntry("values.csv", new String[] { "A", "B" }, DataType.INT16, 250);
                var cff = ve.createCsvFileFormat();
                cff.setSeparator(";");
                cff.setDecimalSeparator(".");
                ve.setFileFormat(cff);
                ve.append(new Value(1320, new short[] { 1, 4 }));
                ve.append(new Value(22968, new short[] { 2, 5 }));
                ve.append(new Value(30232, new short[] { 3, 6 }));
                u.save();
                ShowInfo("Parse file {values.csv} suceed!");

            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            finally
            {
                if (u != null)
                    u.closeAll();
            }
        }

        private void btnVxml_Click(object sender, EventArgs e)
        {
            org.unisens.Unisens u = null;
            try
            {
                UnisensFactory uf = UnisensFactoryBuilder.createFactory();
                u = uf.createUnisens(folderDialogue.SelectedPath);
                ValuesEntry ve = u.createValuesEntry("values.xml", new String[] { "A", "B" }, DataType.INT16, 250);
                ve.setFileFormat(ve.createXmlFileFormat());
                ve.append(new Value(1320, new short[] { 1, 4 }));
                ve.append(new Value(22968, new short[] { 2, 5 }));
                ve.append(new Value(30232, new short[] { 3, 6 }));
                u.save();
                ShowInfo("Parse file {values.xml} suceed!");

            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
            finally
            {
                if (u != null)
                    u.closeAll();
            }
        }

    }
}
