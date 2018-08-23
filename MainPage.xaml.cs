using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Data;

namespace Calculator
{
    /* IMPORTANT NOTE ABOUT EXCEPTIONS
     * 
     * Whenever I run this program using Live Player (emulator is too slow on my machine),
     * the application runs into errors whenever I specify exceptions. So to catch them I
     * just have a generic catch clause otherwise I would have been more specific
     * 
     */

	public partial class MainPage : ContentPage
	{
        StringBuilder answerString = new StringBuilder();
        DataTable evaluator = new DataTable();
        public int MemoryValue { get; set; }
        public int Counter { get; set; }
       
		public MainPage()
		{
			InitializeComponent();   
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            currentExpression.Text += button.Text;
            answerString.Append(button.Text);

            if (button.Text == "=")
            {
                if (currentExpression.Text == "80085=")
                {
                    await DisplayAlert("Developer", "Why don't you grow up?", "Ok");
                }
                try
                {
                    var answer = evaluator.Compute(answerString.ToString().TrimEnd('='), "");
                    currentExpression.Text = answer.ToString();
                    pastExpression.Text = answerString.ToString().TrimEnd('=');
                    answerString.Clear();
                    answerString.Append(answer);
                }
                catch (Exception exception)
                {
                    pastExpression.Text = "Invalid math expression.";
                    currentExpression.Text = string.Empty;
                    answerString.Clear();
                }
            }
        }

        private void Operator_Clicked(object sender, EventArgs e)
        {
            Button _operator = (Button)sender;
            answerString.Append(" " + _operator.Text + " ");
            currentExpression.Text += String.Format(" {0} ", _operator.Text);
        }

        private void Memory_Clicked(object sender, EventArgs e)
        {
            Button memory = (Button)sender;
            switch(memory.Text)
            {
                case ("M+"):
                    try
                    {
                        MemoryValue = int.Parse(currentExpression.Text);
                        break;
                    }
                    catch (Exception exception)
                    {
                        DisplayAlert("Memory Error", "You can only add single numbers into memory", "OK");
                        break;
                    }
                case ("M"):
                    currentExpression.Text = MemoryValue.ToString();
                    answerString.Append(MemoryValue.ToString());
                    break;
                case ("M-"):
                    MemoryValue = 0;
                    break;
            }
        }

        private void Clear_Clicked(object sender,EventArgs e)
        {
            Button clear = (Button)sender;

            switch(clear.Text)
            {
                case ("CLS"):
                    currentExpression.Text = string.Empty;
                    pastExpression.Text = string.Empty;
                    answerString.Clear();
                    break;
                case ("DEL"):
                    try
                    {
                        currentExpression.Text = currentExpression.Text.Substring(0, currentExpression.Text.Length - 1);
                        answerString.Remove(answerString.Length - 1, 1);
                        break;
                    }
                    catch (Exception exception)
                    {
                        break;
                    }
            }
        }
    }
}
