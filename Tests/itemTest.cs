using Sandbox.Common.ObjectBuilders;
using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.Definitions;
using VRageMath;
using ProtoBuf;
using Sandbox.Common.Components;
using System.IO;







[MyEntityComponentDescriptor(typeof(MyObjectBuilder_AngleGrinder))]
public class MyThingLogic : MyGameLogicComponent
{
    public override void Init(MyObjectBuilder_EntityBase objectBuilder)
    {
        if (objectBuilder.SubtypeName == "flag")
        {

        }



        //Sandbox.Definitions.MyHandItemDefinition def;
        //Sandbox.Definitions.MyDefinitionManager mgr;
        ////var t = mgr.GetHandItemDefinitions();
        //// var o = t[0].GetObjectBuilder();
        ////Sandbox.Common.MyObjectFactory<MyEntityComponentDescriptor, MyObjectBuilder_AngleGrinder>();

        ////  Sandbox.Audio.MyCueBank.GetObjectBuilder()
        //Sandbox.Common.ObjectBuilders.Gui.MyObjectBuilder_GuiScreen screen = new Sandbox.Common.ObjectBuilders.Gui.MyObjectBuilder_GuiScreen();
        //// Sandbox.Graphics.GUI.
        ////MyAPIGateway.Entities.CreateFromObjectBuilderAndAdd(screen);
        //Sandbox.ModAPI.Interfaces.IMyCameraController cont;

        //Sandbox.ModAPI.Interfaces.IMyControllableEntity ent;

        //IMyEntity possiblePlayer = null;

        //Sandbox.Graphics.GUI.MyScreenManager.ShowMessageBox("test");

        //IMyEntity entity = possiblePlayer.GetTopMostParent();

        //entity.
        // Sandbox.ModAPI.Ingame.IMyCubeGrid ent = (Sandbox.ModAPI.Ingame.IMyCubeGrid)possiblePlayer;

        //( this.Controller as IMyCameraController ).GetViewMatrix()
        //MyAPIGateway.Session.Player.Controller as IMyCameraController ).GetViewMatrix();
        //        void Rotate( Vector2 rotationIndicator, float rollIndicator );
        //void RotateStopped();

        //IMyEntity srcEntity;

        //HashSet<IMyEntity> _StarGateEntity = new HashSet<IMyEntity>();
        //MyAPIGateway.Entities.GetEntities(_StarGateEntity, e => e is IMyCubeGrid);

        //srcEntity = MyAPIGateway.Entities.GetEntity(e => e.DisplayName == "ship" && e is IMyCubeGrid);
        //IMyCubeGrid grid = (IMyCubeGrid)srcEntity;

        ////grid.GetBlocks

        ////entityToMove.ge


        base.Init(objectBuilder);
    }


    public override MyObjectBuilder_EntityBase GetObjectBuilder(bool copy = false)
    {
        throw new NotImplementedException();
    }
}

//namespace Tests
//{
//    //[MyObjectBuilderDefinition]
//    //[ProtoContract]
//    //public class MyObjectBuilder_Teleporter : MyObjectBuilder_FunctionalBlock
//    //{
//    //    [ProtoMember(1)]
//    //    public float CaptureRadius;

//    //    public MyObjectBuilder_Teleporter();

//    //}

//    [Sandbox.Common.MySessionComponentDescriptor(Sandbox.Common.MyUpdateOrder.BeforeSimulation)]
//    class test : Sandbox.Common.MySessionComponentBase
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
//            MyAPIGateway.Utilities.MessageEntered += Utilities_MessageEntered;
//            _IsLoaded = true;
//            WriteToFile("init triggered");
//        }

//        private void Utilities_MessageEntered(string messageText, ref bool sendToOthers)
//        {
//            if (!messageText.StartsWith("/")) { return; }
//            List<string> words = new List<string>(messageText.Trim().ToLower().Replace("/", "").Split(' '));

//            switch (words[0])
//            {

//                case "entities":
//                    logEntities();
//                    break;
//                case "roid":
//                    createroid(words);
//                    break;
//                case "player":
//                    playerTest();
//                    break;

//                //case "location":
//                //    ReportLocation();
//                //    break;

//                //case "tp":
//                //    Teleport(words);
//                //    break;

//                //case "help":
//                //    break;
//            }
//        }

//        void playerTest()
//        {
//            HashSet<IMyEntity> m_entitiesCache = new HashSet<IMyEntity>();
//            MyAPIGateway.Entities.GetEntities(m_entitiesCache, x => x.Name == "thepenguinmaster");
//            IMyEntity playerEntity = m_entitiesCache.FirstOrDefault();
//            MyObjectBuilder_Character c = (MyObjectBuilder_Character)m_entitiesCache.FirstOrDefault();
//            MyAPIGateway.Utilities.ShowMessage("HAL", c.Health.ToString());
//        }

//        private void createroid(List<string> words)
//        {
//            if (words.Count == 2)
//            {
//                switch (words[1])
//                {
//                    case "help":
//                        MyAPIGateway.Utilities.ShowMessage("HAL", "/roid [x] [y] [z] [optional:roid file]");
//                        MyAPIGateway.Utilities.ShowMessage("HAL", "/roid files");
//                        MyAPIGateway.Utilities.ShowMessage("HAL", "Example: /roid 0 0 0 asteroid2.vx2");
//                        break;
//                    case "files":
//                        // list the roid files
//                        MyAPIGateway.Utilities.ShowMessage("HAL", "Current map files: ");
//                        foreach (var item in Enum.GetNames(typeof(Sandbox.Common.ObjectBuilders.Voxels.MyMwcVoxelFilesEnum)))
//                        {
//                            MyAPIGateway.Utilities.ShowMessage("HAL", item);
//                        }
//                        MyAPIGateway.Utilities.ShowMessage("HAL", "or use rand for a random asteroid");

//                        break;
//                }

//            }
//            if (words.Count > 4)
//            {
//                string roidFile = "asteroid0.vx2";
//                float x, y, z;
//                string xs = words[1];
//                string ys = words[2];
//                string zs = words[3];

//                if (words.Count == 4)
//                {
//                    roidFile = words[4];
//                    // 5th parameter is asteroid file name

//                    if (roidFile == "rand")
//                    {
//                        WriteToFile("rando roid");
//                        // select a rando from the enum
//                        Random random = new Random();
//                        WriteToFile("Created random");
//                        int enumLen = Enum.GetValues(typeof(Sandbox.Common.ObjectBuilders.Voxels.MyMwcVoxelFilesEnum)).Length;
//                        WriteToFile("enumLen: " + enumLen);
//                        roidFile = Enum.GetName(typeof(Sandbox.Common.ObjectBuilders.Voxels.MyMwcVoxelFilesEnum), random.Next(enumLen)) + ".vx2";
//                        WriteToFile("roid file: " + roidFile);
//                    }

//                    WriteToFile("check");
//                    WriteToFile(roidFile.Contains(".vx2").ToString());
//                    if (!roidFile.Contains(".vx2"))
//                    {
//                        WriteToFile("Appending extension");
//                        roidFile += ".vx2";
//                        WriteToFile("Added ext");
//                    }
//                }

//                WriteToFile("Attempting to create roid");
//                MyAPIGateway.Utilities.ShowMessage("HAL", "Attempting to create roid...");
//                if (Single.TryParse(xs, out x) && Single.TryParse(ys, out y) && Single.TryParse(zs, out z))
//                {
//                    Sandbox.Common.ObjectBuilders.Voxels.MyObjectBuilder_VoxelMap vMap;
//                    vMap = new Sandbox.Common.ObjectBuilders.Voxels.MyObjectBuilder_VoxelMap { Filename = roidFile, PositionAndOrientation = new MyPositionAndOrientation(new Vector3(x, y, z), CreateRandomDirection(), CreateRandomDirection()) };
//                    if (vMap != null)
//                    {
//                        vMap.PersistentFlags = MyPersistentEntityFlags2.InScene;
//                        Sandbox.ModAPI.IMyVoxelMap newRoid = (IMyVoxelMap)MyAPIGateway.Entities.CreateFromObjectBuilderAndAdd(vMap);
//                        newRoid.CreatePhysics();
//                        MyAPIGateway.Utilities.ShowMessage("HAL", "Roid created:" + newRoid.VoxelMapId + " Size:" + newRoid.Size.ToString());
//                    }
//                    else
//                    {
//                        MyAPIGateway.Utilities.ShowMessage("HAL", "Failed to create roid");
//                        WriteToFile("Failed to create roid");
//                    }
//                }
//                else
//                {
//                    MyAPIGateway.Utilities.ShowMessage("HAL", "Sorry, that location is no good.");


//                }
//            }

//        }

//        Vector3 CreateRandomDirection()
//        {
//            Random rand = new Random();
//            return new Vector3(rand.Next(0, 100) / 100, rand.Next(0, 100) / 100, rand.Next(0, 100) / 100);
//        }

//        private void logEntities()
//        {


//            HashSet<IMyEntity> m_entitiesCache = new HashSet<IMyEntity>();

//            foreach (MyObjectBuilder_EntityBase b in Sandbox.ModAPI.MyAPIGateway.Session.GetWorld().Sector.SectorObjects)
//            {
//                WriteToFile(b.EntityId.ToString());
//            }
//            // get all the things and log them..



//            MyAPIGateway.Entities.GetEntities(m_entitiesCache, x => x.EntityId == x.EntityId);
//            foreach (IMyEntity e in m_entitiesCache)
//            {
//                WriteToFile(e.EntityId.ToString());
//                WriteToFile(e.DisplayName);
//                WriteToFile(e.Name);
//                e.SetPosition(new Vector3(0, 0, 0));

//                WriteToFile("X " + e.GetPosition().X + "Y " + e.GetPosition().Y + "Z " + e.GetPosition().Z);

//                MyAPIGateway.Utilities.ShowMessage("HAL", e.EntityId.ToString());
//                MyAPIGateway.Utilities.ShowMessage("HAL", "X " + e.GetPosition().X + "Y " + e.GetPosition().Y + "Z " + e.GetPosition().Z);
//            }


//            //var station = MyAPIGateway.Entities.GetEntity((x) => x is IMyCubeGrid && x.DisplayName == "Station1") as IMyCubeGrid;
//        }

//        private void WriteToFile(string text)
//        {
//            string path = @"c:\test.txt";

//            using (StreamWriter sw = File.AppendText(path))
//            {
//                sw.WriteLine(text);
//            }
//        }
//    }

//}
