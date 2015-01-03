using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces;
using Sandbox.Game;
using System.IO;

namespace Teleporter
{
    //public class Teleporter
    //{
    //    public void main(){
    //        Sandbox.Graphics.MyGuiManager.GetScreenshot();
    //    }
    //}


    [Sandbox.Common.MySessionComponentDescriptor(Sandbox.Common.MyUpdateOrder.BeforeSimulation)]
    class Teleporter : Sandbox.Common.MySessionComponentBase
    {
        private bool _IsLoaded = false;

        public override void UpdateBeforeSimulation()
        {
            if (!_IsLoaded)
            {
                init();
            }

        }

        public void init()
        {
            Sandbox.Common.ObjectBuilders.Definitions.SerializableDefinitionId id;
            //Sandbox.Common.ObjectBuilders.Serializer.MyObjectBuilderSerializer.createnew;
            _IsLoaded = true;
            MyAPIGateway.Utilities.MessageEntered += Utilities_MessageEntered;
        }

        private void Utilities_MessageEntered(string messageText, ref bool sendToOthers)
        {
            log("test");
            if (!messageText.StartsWith("/")) { return; }
            List<string> words = new List<string>(messageText.Trim().ToLower().Replace("/", "").Split(' '));

            sendToOthers = false;

            switch (words[0])
            {
                case "report":
                    MyAPIGateway.Utilities.ShowMessage("HAL", "Mod Loaded: StarGate");
                    sendToOthers = false;
                    break;
                case "test":
                    log("Start test");
                    //Sandbox.Graphics.MyGuiManager.GetScreenshot();
                    log("test1");
                    Sandbox.Graphics.GUI.MyScreenManager.GetScreensCount();
                    log("test2");
                    break;

            }
        }

        private void log(string text)
        {
            using (TextWriter writer = MyAPIGateway.Utilities.WriteFileInGlobalStorage("Log.txt"))
            {
                writer.WriteLine(text);
            }
        }
    }
}
