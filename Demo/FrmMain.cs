using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PSNLib;
using PSNLib.Models;
using static System.Threading.Tasks.Task;
using static System.IO.File;

namespace Demo
{
    public partial class FrmMain : Form
    {
        private PSAPI _ps;
        private List<Friends> _friendList;
        private int _playerCount;
        private const string DelFilePath = "deleted.txt";
        private void PrintCount(int inc, int allCount = -1)
        {
            allCount = allCount < 0 ? _playerCount : allCount;
            lblFriendsCount.Text = $"Count: {inc}/{allCount}";

        }
        class Friends
        {
            public string Name{ get; set; }
            public int Id{ get; set; }
        }
        private void PicBox(bool state)
        {
            pictureBox.Visible = state;
        }
        public FrmMain()
        {
            #region From
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            #endregion
            AppendAllText(DelFilePath, "---\n");
            #region ContextMenuStrip cms
            ContextMenuStrip cms = new ContextMenuStrip();
            cms.Items.Add("Delete");
            cms.Click += delegate
            {
                Execute(() => {
                    foreach (ListViewItem friend in listView.SelectedItems)
                    {
                        var friendName = friend.SubItems[1].Text;
                        if (_ps.Friend.Delete(friendName))
                        {
                            friend.Remove();
                            PrintCount(listView.Items.Count, --_playerCount);
                            AppendAllText(DelFilePath, friendName + "\n");
                        }
                    }
                    AppendAllText(DelFilePath, "---\n");
                });
                
            }; 
            #endregion
            #region listView
            listView.View = View.Details;
            listView.GridLines = true;
            listView.Columns.Add("*", 70);
            listView.Columns.Add("PSN", listView.Size.Width);
            listView.ContextMenuStrip = cms;
            listView.KeyDown += (sender, e) =>
            {
                if (e.KeyCode != Keys.A || !e.Control) return;
                listView.MultiSelect = true;
                foreach (ListViewItem item in listView.Items)
                {
                    item.Selected = true;
                }
            };
            #endregion
            #region txtFilterBox

            txtFilterBox.TextChanged += delegate
            {
                listView.Items.Clear();
                var lists = (from item in _friendList
                    where item.Name.ToLower().Contains(txtFilterBox.Text.ToLower())
                    select item);
                int num = 1;
                lists.ToList().ForEach(x =>
                {
                    PrintCount(num++);
                    listView.Items.Add(new ListViewItem(new[] {x.Id.ToString(), x.Name}));
                });
            };

            #endregion
        }
        private bool LogIn()
        {

            try
            {
                _ps = new PSAPI(txtEmailBox.Text, txtPassBox.Text);
                lblUsername.Text = _ps.Profile.OnlineId;
                return _ps != null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        private void RetrieveFriends()
        {

            listView.Items.Clear();
            if (_ps == null)
                return;
            FriendsFilter ff = (FriendsFilter) (chkOnlyOnline.Checked ? 0 : 1);
            _playerCount = _ps.Friend.Count(ff);
            PrintCount(0);

            int inc = 1;
            _friendList = new List<Friends>();

            var allFriends = _ps.Friend.AllFriends(0, _playerCount, ff);
            foreach (var item in allFriends)
            {
                if (item == null)
                    continue;
                _friendList.Add(new Friends() {Id = inc++, Name = item.OnlineId});
            }
            int num = 1;
            _friendList.ToList().ForEach(x =>
            {
                PrintCount(num++);
                listView.Items.Add(new ListViewItem(new[]{ x.Id.ToString(), x.Name}));
            });


        }

        private void LockMe()
        {
            bool stat = btnLogin.Enabled;
            btnLogin.Enabled = !stat;
            PicBox(stat);
            Controls.OfType<Control>().ToList().ForEach(x =>
            {
                if (x.GetType() != typeof(PictureBox))
                    x.Enabled = !stat;
            });
        }

        private async void Execute(Action act)
        {
            LockMe();
            await Run(act);
            LockMe();
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Execute(() =>
            {
                if (LogIn())
                     RetrieveFriends();
            });
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            
            Execute(RetrieveFriends);
        }
    }
}
