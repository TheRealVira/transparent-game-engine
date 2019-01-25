using System;
using System.Linq;
using System.Windows.Forms;
using TransparentGameEngine;

namespace ExampleUseCases
{
    public partial class SelectionMenu : Form
    {
        private TransparentGameEngine.Game MyGameComponent;

        public SelectionMenu()
        {
            InitializeComponent();

            var type = typeof(TransparentGameComponent);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract);

            foreach (var type1 in types)
            {
                listBox1.Items.Add(type1);
            }

            MyGameComponent = new TransparentGameEngine.Game(TransparentGameComponent.Generate((Type)listBox1.Items[0]), new Win32Window(Handle));
            
            MyGameComponent.Run();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem!=null)
                MyGameComponent.LoadGameComponent(TransparentGameComponent.Generate((Type) listBox1.SelectedItem));
        }
    }
}
