//using Sandbox.ModAPI;
//using Sandbox.Common;
//using Sandbox.Common.ObjectBuilders;
//using Sandbox.Common.Components;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using LitJson;
//using System.IO;


//namespace scripts.ThePenuginMaster
//{
//    [Sandbox.Common.MySessionComponentDescriptor(Sandbox.Common.MyUpdateOrder.BeforeSimulation)]
//    class MissionComponent : Sandbox.Common.MySessionComponentBase
//    {
//        private bool _IsLoaded = false;
//        private bool _DisplayMOTD = false;
//        private MOTDEntry entry;

//        public override void UpdateBeforeSimulation()
//        {
//            if (!_IsLoaded)
//            {
//                WriteToFile("init");
//                init();
//            }

//            if (MyAPIGateway.Session != null)
//            {

//                //foreach (MOTDEntry entry in entries)
//                //{
//                if (entry != null)
//                {
//                    if (entry.DisplayOnLogin)
//                    {
//                        MyAPIGateway.Utilities.ShowMessage(entry.DisplayFrom, entry.Message);
//                        entry.DisplayOnLogin = false;
//                    }
//                }
//                //}
//            }
//        }

//        public void init()
//        {
//            try
//            {
//                //MyAPIGateway.Utilities.ShowNotification("Loading test mod");
//                MyAPIGateway.Utilities.MessageEntered += Utilities_MessageEntered;
//                MOTDEntry entry;
//                string motdText = "";

//                WriteToFile("event setup");
//                TextReader reader = MyAPIGateway.Utilities.ReadFileInGlobalStorage("MOTD");
//                WriteToFile("Reader loaded");

//                if (reader == null)
//                {
//                    WriteToFile("Reader is null!");
//                    motdText = "";
//                }
//                else
//                {
//                    motdText = reader.ReadToEnd();
//                }

//                WriteToFile(motdText);
//                WriteToFile("Read MOTD Text from reader");

//                if (motdText != "")
//                {
//                    entry = LoadMOTD(motdText);
//                }
//            }
//            catch
//            {
//                WriteToFile("Some error i cant check because there is no reflection :(");
//            }
//            _IsLoaded = true;
//        }



//        private MOTDEntry LoadMOTD(string motdText)
//        {
//            JsonData data = JsonMapper.ToObject(motdText);
//            WriteToFile(data[0]["Message"].ToString());

//            return new MOTDEntry(data["Message"].ToString(), data["DisplayFrom"].ToString(), Convert.ToBoolean(data["DisplayOnLogin"].ToString()), Convert.ToBoolean(data["DisplayAtInterval"].ToString()), Convert.ToInt32(data["DisplayIntervalMins"].ToString()));
//        }

//        private void WriteToFile(string text)
//        {
//            string path = @"c:\test.txt";

//            using (StreamWriter sw = File.AppendText(path))
//            {
//                sw.WriteLine(text);
//            }
//        }


//        private void Utilities_MessageEntered(string messageText, ref bool sendToOthers)
//        {
//            WriteToFile(messageText);
//            switch (messageText.ToLower())
//            {

//                case "hello hal":
//                    MyAPIGateway.Utilities.ShowMessage("HAL", "Hello Dave");

//                    break;
//                case "reset":
//                    _IsLoaded = false;
//                    MyAPIGateway.Utilities.ShowMessage("HAL", "Good bye Dave");
//                    break;
//                case "location":
//                    //MyAPIGateway.Utilities.ShowMessage("HAL", "X: " + MyAPIGateway.Session.GetSector().Position.X + " Y: " + MyAPIGateway.Session.GetSector().Position.Y + " Z: " + MyAPIGateway.Session.GetSector().Position.Z);

//                    MyAPIGateway.Utilities.ShowMessage("HAL", "X: " + MyAPIGateway.Session.Player.PlayerCharacter.Entity.GetPosition().X + " Y: " + MyAPIGateway.Session.Player.PlayerCharacter.Entity.GetPosition().Y + " Z: " + MyAPIGateway.Session.Player.PlayerCharacter.Entity.GetPosition().Z);
//                    break;
//                case "screen":
                    
//                    //Sandbox.Graphics.GUI.MyScreenManager.AddScreen(new TestScreen());
//                    break;
//            }
//        }


//        //class TestScreen : Sandbox.Graphics.GUI.MyGuiScreenBase
//        //{
//        //    //public override void LoadContent()
//        //    //{
//        //    //    VRageMath.Vector2 pos = new VRageMath.Vector2(10, 10);
//        //    //    VRageMath.Vector2 size = new VRageMath.Vector2(100, 50);
//        //    //    //StringBuilder text = new StringBuilder("test");
//        //    //    //Sandbox.Graphics.GUI.MyGuiControlLabel label = new Sandbox.Graphics.GUI.MyGuiControlLabel(pos, size, text);
//        //    //    //Controls.Add(label);
//        //    //    //this.Controls.Add(new Sandbox.Graphics.GUI.MyGuiControlButton(new VRageMath.Vector2(10, 10), Sandbox.Common.ObjectBuilders.Gui.MyGuiControlButtonStyleEnum.Close, new VRageMath.Vector2(100, 100), null, VRage.MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_CENTER, new StringBuilder("test"), new StringBuilder("test")));

//        //    //    //base.LoadContent();
//        //    //}

//        //    public override string GetFriendlyName()
//        //    {
//        //       // throw new NotImplementedException();
//        //        return "test dialog";
//        //    }
//        //}


//        public class MOTDEntry
//        {
//            public MOTDEntry() { }
//            public MOTDEntry(string message, string displayFrom, bool displayOnLogin, bool displayAtInterval, int displayIntervalMins)
//            {
//                Message = message;
//                displayFrom = DisplayFrom;
//                displayOnLogin = DisplayOnLogin;
//                displayAtInterval = DisplayAtInterval;
//                displayIntervalMins = DisplayIntervalMins;
//            }

//            public string Message;
//            public string DisplayFrom;
//            public bool DisplayOnLogin;
//            public bool DisplayAtInterval;
//            public int DisplayIntervalMins;

//            //[NonSerialized]
//            public DateTime LastDisplayTime;
//        }
//    }




//}