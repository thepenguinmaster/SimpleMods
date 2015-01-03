//using Sandbox.Common.ObjectBuilders;
//using Sandbox.ModAPI;
//using Sandbox.ModAPI.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Sandbox.Definitions;
//using VRageMath;
//using ProtoBuf;
//using Sandbox.Common.Components;
//using System.IO;



//namespace Tests
//{
//    [MyEntityComponentDescriptor(typeof(MyObjectBuilder_AngleGrinder))]
//    public class MyThingLogic : MyWeaponItemDefinition
//    {
//        public override void Init(Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_DefinitionBase builder)
//        {
//            base.Init(builder);
//        }

//    }


//    [Sandbox.Common.MySessionComponentDescriptor(Sandbox.Common.MyUpdateOrder.BeforeSimulation)]
//    class UITest : Sandbox.Common.MySessionComponentBase
//    {

//        private bool _IsLoaded = false;


//        public override void UpdateBeforeSimulation()
//        {
//            if (!_IsLoaded)
//            {
//                init();
//            }


//        }

//        public void init()
//        {

//            log("loading");
//            _IsLoaded = true;
//            MyAPIGateway.Utilities.MessageEntered += Utilities_MessageEntered;
//        }

//        private void Utilities_MessageEntered(string messageText, ref bool sendToOthers)
//        {
//            log("Message entered: " + messageText);
//            if (!messageText.StartsWith("/")) { return; }
//            List<string> words = new List<string>(messageText.Trim().ToLower().Replace("/", "").Split(' '));

//            sendToOthers = false;

//            switch (words[0])
//            {
//                case "report":
//                    MyAPIGateway.Utilities.ShowMessage("HAL", "Mod Loaded: Tests");
//                    sendToOthers = false;
//                    break;
//                case "test":
//                    test();
//                    MyAPIGateway.Utilities.ShowMessage("HAL", "Test complete");
//                    break;
//            }
//        }

//        private void test()
//        {
//            //Sandbox.Graphics.GUI.MyScreenManager.ShowMessageBox(Sandbox.Common.Localization.MyTextsWrapperEnum.BlockPropertyTitle_GyroOverride, Sandbox.Common.Localization.MyTextsWrapperEnum.BlockOwner_Me, Sandbox.Graphics.GUI.MyMessageBoxStyleEnum.BLUE);

//            //Sandbox.Graphics.GUI.MyScreenManager.ShowMessageBox(Sandbox.Common.Localization.MyTextsWrapperEnum.BlockPropertyTitle_GyroPower);

//            //Sandbox.MyGuiSoundManager.PlaySound(Sandbox.GuiSounds.MouseOver);

//            VRageMath.

//        }


//        private void log(string text)
//        {
//            using (TextWriter writer = MyAPIGateway.Utilities.WriteFileInGlobalStorage("Log.txt"))
//            {
//                writer.WriteLine(text);
//            }
//        }

//        protected override void UnloadData()
//        {
//            MyAPIGateway.Utilities.MessageEntered -= Utilities_MessageEntered;
//            base.UnloadData();
//        }

//    }
//}
