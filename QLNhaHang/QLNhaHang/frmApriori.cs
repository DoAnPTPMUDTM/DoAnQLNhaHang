using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLLDAL;
using Accord.MachineLearning.Rules;

namespace QLNhaHang
{
    public partial class frmApriori : Form
    {
        HoaDonBLLDAL hoaDonBLLDAL = new HoaDonBLLDAL();
        public frmApriori()
        {
            InitializeComponent();
        }
        public void load()
        {
            SortedSet<int>[] dataset =
                {
                //new SortedSet<int> { 1, 2, 3, 4 }, 
                //new SortedSet<int> { 1, 2, 4 },   
                //new SortedSet<int> { 1, 2 },      
                //new SortedSet<int> { 2, 3, 4 },   
                //new SortedSet<int> { 2, 3 },
                //new SortedSet<int> { 3, 4 },
                //new SortedSet<int> { 3, 4 },
                //new SortedSet<int> { 2, 4 },
                //new SortedSet<int> { 1, 2, 3},
                //new SortedSet<int> { 1, 2, 3 },
                //new SortedSet<int> { 1, 2 ,3},
                //new SortedSet<int> { 2, 3, 3 },
                //new SortedSet<int> {1, 2, 3 },
                //new SortedSet<int> {1, 2, 3 },
                //new SortedSet<int> {1, 2, 3 },
                //new SortedSet<int> {1, 2, 3 },
            };
            Apriori apriori = new Apriori(threshold: 2, confidence: 0.6);
            MessageBox.Show(hoaDonBLLDAL.getDataSet().Length.ToString());
            AssociationRuleMatcher<int> classifier = apriori.Learn(hoaDonBLLDAL.getDataSet());
            MessageBox.Show( "dataset: " + hoaDonBLLDAL.getDataSet().Count().ToString());
            int[][] matches = classifier.Decide(new[] { 1,2 });
            List<int> lstKetQua = new List<int>();
            List<Result> lstResult = new List<Result>();

            for (int i = 0; i < matches.Length; i++)
            {
                string rs = "{";
                for(int j = 0; j < matches[i].Length; j++)
                {
                    MessageBox.Show(matches[i][j].ToString());
                    rs += matches[i][j].ToString() + " ";
                    lstKetQua.Add(matches[i][j]);
                }
                rs += "}";
                Result rss = new Result(rs);
                lstResult.Add(rss);
            }

            dataGridView1.DataSource = lstResult;
            List<Result> lstRS = new List<Result>();
            AssociationRule<int>[] rules = classifier.Rules;
            for(int k = 0; k < rules.Length; k++)
            {
                Result rs = new Result(rules[k].ToString());
                lstRS.Add(rs);
            }
            dataGridView2.DataSource = lstRS;
        }
        public class Result
        {
            public string KetQua { get; set; }
            public Result()
            {

            }
            public Result(string rs)
            {
                this.KetQua = rs;
            }
        }
        private void frmApriori_Load(object sender, EventArgs e)
        {
            load();
        }
    }
}
