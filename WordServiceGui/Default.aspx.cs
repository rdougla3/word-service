using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WordServiceGui
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            WordService.Service1Client wordService = new WordService.Service1Client();
            Label1.Text = wordService.WordFilter(TextBox1.Text);
            wordService.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            WordService.Service1Client wordService = new WordService.Service1Client();
            string[] top10 = wordService.Top10Words(TextBox2.Text);
            Label2.Text = String.Join(", ", top10);
            wordService.Close();
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            WordService.Service1Client wordService = new WordService.Service1Client();
            LCAlabel.Text = wordService.Hypernym(word1.Text, word2.Text);
            wordService.Close();
        }
    }
}