using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WinForms_HW_4
{
    public partial class Form1 : Form
    {
        public List<Ankette> ankettes = new List<Ankette>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FileStream stream = new FileStream("Ankettes.xml", FileMode.Create, FileAccess.Write);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Ankette>));
            serializer.Serialize(stream, ankettes);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ankette ankette = new Ankette(textBox_Name.Text, textBox_Surname.Text, textBox_Email.Text, textBox_Phone.Text);
            ankettes.Add(ankette);
            listBox1.DisplayMember = "Name";
            listBox1.Items.Add(ankette);
            
            ClearTextBoxes();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                listBox1.ClearSelected();
            else
            {
                Ankette ankette = listBox1.SelectedItem as Ankette;
                textBox_Name.Text = ankette.Name;
                textBox_Surname.Text = ankette.Surname;
                textBox_Email.Text = ankette.Email;
                textBox_Phone.Text = ankette.Phone;
            }


        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            Ankette ankette = listBox1.SelectedItem as Ankette;
            foreach (var item in ankettes)
                if (ankette.Equals(item))
                {
                    ankettes.Remove(item);
                    break;
                }

            listBox1.Items.Remove(listBox1.SelectedItem);
            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            textBox_Name.Text = string.Empty;
            textBox_Surname.Text = string.Empty;
            textBox_Phone.Text = string.Empty;
            textBox_Email.Text = string.Empty;
        }

        private void button_ChangeProfile_Click(object sender, EventArgs e)
        {
            Ankette ankette = listBox1.SelectedItem as Ankette;
            foreach (var item in ankettes)
                if (ankette.Equals(item))
                {
                    item.Name = textBox_Name.Text;
                    item.Surname = textBox_Surname.Text;
                    item.Phone = textBox_Phone.Text;
                    item.Email = textBox_Email.Text;
                    break;
                }

            listBox1.SelectedItem = ankette;
            ClearTextBoxes();
        }
    }

    [Serializable]
    [DataContract]
    public class Ankette
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Phone { get; set; }

        public Ankette() { }
        public Ankette(string name, string surname, string email, string phone)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
        }
    }
}
