using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calc_Step1
{
    public partial class frmCalculator : Form
    {
        public frmCalculator()
        {
            InitializeComponent();
        }

        //Declaring variables
        //clearDisplay being true makes the calculator act as if there's just a 0 in the textbox.
        Boolean clearDisplay = true;
        //isFirstValue is used to check whether we've entered a previous value. generally if it's 
        //false then we perform operations on the values entered
        Boolean isFirstValue = true;
        //isAfterEquals is used to check whether the equals button is pressed several times in a row.
        //If it is then the operation would be repeated using the same lastValueEntered
        Boolean isAfterEquals = false;
        //memoryValue is used to record a single value which is able to be recalled later on
        Double memoryValue = 0.0;
        //currentAnswer is used to store the running total after operations. This is what's written 
        //to the textbox
        Double currentAnswer;
        //lastValueEntered is used to store the value written in the textbox at the time either the
        //operation button or equals is clicked
        Double lastValueEntered;
        //Used to store which operation button was clicked
        Char lastOp;

        private void btn_Click(object sender, EventArgs e)
        {
            //These two lines are used to set the digit variable to the number written on the button
            Button button = (Button)sender;
            String digit = button.Text;

            //Checking to see if there's anything in the textbox which isn't the previous number
            if(clearDisplay)
            {
                //Put the value stored in digit into the textbox
                txtDisplay.Text = digit;
                //Make clearDisplay false
                clearDisplay = false;
            }
            else
            {
                //Checking to make sure the number is below 26 characters
                if (txtDisplay.Text.Length < 26)
                {
                    //Append the value stored in digit onto the end of the string
                    txtDisplay.AppendText(digit);
                }
                
            }

            //Checking to see if equals was the last button pressed
            if (isAfterEquals)
            {
                //Make the current final value = '0.0'
                currentAnswer = 0.0;
                //Make lastOp = ' '
                lastOp=' ';
            }

            //Make isAfterEquals false
            isAfterEquals = false;
        }

        private void btnDP_Click(object sender, EventArgs e)
        {
            //Checking to see if there's anything in the textbox which isn't the previous number
            if (clearDisplay)
            {
                //Make the number in the textbox '0.'
                txtDisplay.Text = "0.";
                //Make clearDisplay false
                clearDisplay = false;
            }
            else
            {
                //Add a '.' to the end of the number in the textbox
                txtDisplay.AppendText(".");
            }

            //Checking to see if equals was the last button pressed
            if (isAfterEquals)
            {
                //Make the current final value = '0.0'
                currentAnswer = 0.0;
                //Make lastOp = ' '
                lastOp = ' ';
            }

            //Make isAfterEquals false
            isAfterEquals = false;
            //Disable the decimal point button
            btnDP.Enabled = false;  
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Block of code to reset all variable back to the default states and
            //enable the decimal point button
            txtDisplay.Text = "0";
            clearDisplay = true;
            btnDP.Enabled = true;
            isFirstValue = true;
            isAfterEquals = false;
            currentAnswer = 0.0;
            lastOp=' ';
        }

        private void btnMadd_Click(object sender, EventArgs e)
        {
            //Add what's in the display to the value stored in memory
            memoryValue += double.Parse(txtDisplay.Text);
        }

        private void btnMsub_Click(object sender, EventArgs e)
        {
            //Take what's in the display away from the value stored in memory
            memoryValue -= double.Parse(txtDisplay.Text);
        }

        private void btnMclr_Click(object sender, EventArgs e)
        {
            //Clear the memory value
            memoryValue = 0.0;
        }

        private void btnMrec_Click(object sender, EventArgs e)
        {
            //Display the value stored in memory in the textbox
            txtDisplay.Text = memoryValue.ToString();
        }

        private void btnBksp_Click(object sender, EventArgs e)
        {
            //Checking whether there's more than one digit in the string
            if(txtDisplay.Text.Length > 1)
            {
                //Checking to see if the string ends with a decimal point
                if(txtDisplay.Text.EndsWith("."))
                {
                    //Enabling the decimal point button
                    btnDP.Enabled = true;
                }
                //Removing the last digit from the string
                txtDisplay.Text = txtDisplay.Text.Substring(0, (txtDisplay.Text.Length - 1));
            }
            else
            {
                //Make the string '0.0'
                txtDisplay.Text = "0";
            }
        }

        //Making a function called 'op_Button'
        private void op_Button()
        {
            //Making clearDisplay true
            clearDisplay = true;
            //Enabling the decimal point button
            btnDP.Enabled = true;

            //Checking whether the string in the textbox is the first value entered
            if (isFirstValue)
            {
                //Storing the string as a number in currentAnswer
                currentAnswer = double.Parse(txtDisplay.Text);
                //Making isFirstValue false
                isFirstValue = false;
            }
            else
            {
                //Storing the string in the textbox in lastValueEntered
                lastValueEntered = double.Parse(txtDisplay.Text);
                //Starting switch case on the variable lastOp
                switch (lastOp)
                {
                    //Seeing if lastOp is a '+'
                    case '+':
                        //Add lastValueEntered to currentAnswer
                        currentAnswer += lastValueEntered;
                        //'break' is used to break out of the switch case after a match ahs been found.
                        break;
                    //Seeing if lastOp is a '-'
                    case '-':
                        //Take lastValueEntered away from currentAnswer
                        currentAnswer -= lastValueEntered;
                        break;
                    //Seeing if lastOp is a '*'
                    case '*':
                        //Multiply currentAnswer by lastValueEntered
                        currentAnswer *= lastValueEntered;
                        break;
                    //Seeing if lastOp is a '/'
                    case '/':
                        //Divide currentAnswer by lastValueEntered
                        currentAnswer /= lastValueEntered;
                        break;
                }
            }

            //Making isAfterEquals false
            isAfterEquals = false;
            //Writing currentAnswer into the textbox
            txtDisplay.Text = currentAnswer.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Run the function 'op_Button'
            op_Button();
            //Make lastOp = '+'
            lastOp = '+';
        }       


        private void btnSub_Click(object sender, EventArgs e)
        {
            //Run the function 'op_Button'
            op_Button();
            //Make lastOp = '-'
            lastOp = '-';
        }       
        

        private void btnMult_Click(object sender, EventArgs e)
        {
            //Run the function 'op_Button'
            op_Button();
            //Make lastOp = '*'
            lastOp = '*';
        }       

        private void btnDiv_Click(object sender, EventArgs e)
        {
            //Run the function 'op_Button'
            op_Button();
            //Make lastOp = '/'
            lastOp = '/';
        }        
        private void btnEquals_Click(object sender, EventArgs e)
        {
            //Check whether this is the first value entered
            if (isFirstValue)
            {
                //Store the string in the textbox in currentAnswer
                currentAnswer = double.Parse(txtDisplay.Text);
            }

            //Check whether the last button pressed was equals
            if (isAfterEquals == false)
            {
                //Storing the string in the textbox in lastValueEntered
                lastValueEntered = double.Parse(txtDisplay.Text);
            }

            //Making isAfterEquals true
            isAfterEquals = true;

            //Starting switch case on the variable lastOp
            switch (lastOp)
            {
                //Seeing if lastOp is a '+'
                case '+':
                    //Add lastValueEntered to currentAnswer
                    currentAnswer += lastValueEntered;
                    //'break' is used to break out of the switch case after a match ahs been found.
                    break;
                //Seeing if lastOp is a '-'
                case '-':
                    //Take lastValueEntered away from currentAnswer
                    currentAnswer -= lastValueEntered;
                    break;
                //Seeing if lastOp is a '*'
                case '*':
                    //Multiply currentAnswer by lastValueEntered
                    currentAnswer *= lastValueEntered;
                    break;
                //Seeing if lastOp is a '/'
                case '/':
                    //Divide currentAnswer by lastValueEntered
                    currentAnswer /= lastValueEntered;
                    break;
            }

            //Display the final answer in the textbox
            txtDisplay.Text = currentAnswer.ToString();
            //Makes isFirstValue true
            isFirstValue = true;
            //Enables btnDP
            btnDP.Enabled = true;
            //Makes clearDisplay true
            clearDisplay = true;
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            //switch the number displayed in the string of the textbox between positive and negative.
            //This is achieved by taking double the value away from itself.
            txtDisplay.Text = (double.Parse(txtDisplay.Text) - (double.Parse(txtDisplay.Text) * 2)).ToString();
        }

    }
}

