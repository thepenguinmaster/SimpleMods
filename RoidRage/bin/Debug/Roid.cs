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

//namespace ThePenguinMaster.Mods.RoidRage
//{
//    // You know, if your gonna copy, why not give some credit? 
//    [Sandbox.Common.MySessionComponentDescriptor(Sandbox.Common.MyUpdateOrder.BeforeSimulation)]
//    class RoidRage : Sandbox.Common.MySessionComponentBase
//    {
//        private bool _IsLoaded = false;
//        private int callCount = 0;
//        private string VoxelFilePath = "";
//        private Sandbox.ModAPI.IMyVoxelMap hellRoid;
//        float rad = 0;
//        float roidSpeed = .00001f;
//        public override void UpdateBeforeSimulation()
//        {
//            if (!_IsLoaded)
//            {
//                init();
//            }
//            if (hellRoid != null)
//            {
//                // move roid along X axis
//                IMyEntity ent = ((IMyEntity)hellRoid);
//                Vector3 curPos = ent.GetPosition();
//                curPos.X += 0.5f;

//                curPos.X = ((float)Math.Sin(rad ) * 1000f);
//                curPos.Y = ((float)Math.Cos(rad ) * 1000f);
//                ent.SetPosition(curPos);
//                if (rad > 360) { rad = 0; }
//                rad += roidSpeed;
//            }
//        }

//        public void init()
//        {
//            _IsLoaded = true;
//            LoadPath();
//            MyAPIGateway.Utilities.MessageEntered += Utilities_MessageEntered;
//        }

//        private void Utilities_MessageEntered(string messageText, ref bool sendToOthers)
//        {
//            if (!messageText.StartsWith("/")) { return; }
//            List<string> words = new List<string>(messageText.Trim().ToLower().Replace("/", "").Split(' '));
//            sendToOthers = false;
//            switch (words[0])
//            {
//                case "report":
//                    MyAPIGateway.Utilities.ShowMessage("HAL", "Mod Loaded: RoidRage");
//                    break;
//                case "hello":
//                    SayHello(words);
//                    break;
//                case "roid":
//                    createroid(words);
//                    break;
//                case "orbital":
//                    roidhell(words);
//                    break;
//                case "roidspeed":
//                    setRoidSpeed(words);
//                    break;

//                case "setpath":
//                    SetPath(messageText.Trim().ToLower().Replace("/", ""));
//                    break;
//                case "help":
//                    MyAPIGateway.Utilities.ShowMessage("HAL", "/report - Reports all loaded mods");
//                    MyAPIGateway.Utilities.ShowMessage("HAL", "/roid - Manage asteroids ");
//                    MyAPIGateway.Utilities.ShowMessage("HAL", "/setpath - sets the voxel file path");
//                    MyAPIGateway.Utilities.ShowMessage("HAL", "/help ");

//                    break;
//            }
//        }

//        void setRoidSpeed(List<string> words)
//        {
//            if (words.Count > 1)
//            {
//                float.TryParse(words[1], out roidSpeed);
//            }
//        }

//        void roidhell(List<string> words)
//        {
//            MyAPIGateway.Utilities.ShowMessage("HAL", "roid hell activating");
//            //PirateBaseStaticAsteroid_A_5000m_2
//            Sandbox.ModAPI.IMyVoxelMap newRoid;
//            newRoid = PlaceRoid(VoxelFilePath + "PirateBaseStaticAsteroid_A_5000m_2.vx2", new MyPositionAndOrientation(MyAPIGateway.Session.Player.PlayerCharacter.Entity.GetPosition(), Vector3.Up, Vector3.Up));
//            if (newRoid != null)
//            {
//                hellRoid = newRoid;
//                //newRoid.//ReadRange( hellRoid.Storage, VRage.Voxels.MyStorageDataTypeFlags.ContentAndMaterial, VRageRender.MyLodTypeEnum.LOD_NEAR, )
//                //hellRoid.CreatePhysics();
//                if (words.Count > 1)
//                {
//                    float.TryParse(words[1], out roidSpeed);
//                }
//            }
//        }

//        void SetPath(string message)
//        {
//            // get the path
//            string path = message.Replace("setpath", "").Trim();

//            if (!path.EndsWith(@"\"))
//            {
//                path += @"\";
//            }
//            if (!path.Contains("Content"))
//            {
//                path += @"Content\VoxelMaps\";
//            }

//            // the remainder should be the path
//            //if (!File.Exists(path + "small2_asteroids.vx2"))
//           // {
//                MyAPIGateway.Utilities.ShowMessage("HAL", "Invalid path. Please specify the absolute path to your space engineers install.");
//                MyAPIGateway.Utilities.ShowMessage("HAL", @"Example: C:\Program Files\Steam\SteamApps\common\SpaceEngineers\");
//            //}

//            // save the path for the future
//            using (TextWriter writer = MyAPIGateway.Utilities.WriteFileInLocalStorage("Settings", typeof(RoidRage)))
//            {

//                if (writer != null)
//                {
//                    writer.Write(path);
//                    VoxelFilePath = path;
//                }
//                else
//                {
//                    VoxelFilePath = "";
//                }
//            }
//        }

//        void LoadPath()
//        {
//            try
//            {
//                TextReader reader = MyAPIGateway.Utilities.ReadFileInLocalStorage("Settings", typeof(RoidRage));
//                if (reader == null)
//                {
//                    VoxelFilePath = "";
//                }
//                else
//                {
//                    VoxelFilePath = reader.ReadToEnd();
//                }
//            }
//            catch
//            {
//                VoxelFilePath = "";
//            }
//        }

//        void SayHello(List<string> words)
//        {
//            switch (words[1])
//            {
//                case "hal":
//                    MyAPIGateway.Utilities.ShowMessage("HAL", "Hello Dave.");
//                    break;
//                case "scotty":
//                    MyAPIGateway.Utilities.ShowMessage("Scotty", "I'm giving her all she's got, Captain!");
//                    break;
//                case "cortana":
//                    MyAPIGateway.Utilities.ShowMessage("Scotty", "One moment, sir. Accessing Covenant Battlenet.");
//                    break;
//            }
//        }

//        // I seeeee you
//        private void createroid(List<string> words)
//        {
//            Sandbox.ModAPI.IMyVoxelMap newRoid;

//            if (words.Count == 2)
//            {
//                switch (words[1])
//                {
//                    case "help":
//                        MyAPIGateway.Utilities.ShowMessage("HAL", "/roid [x] [y] [z] [optional:roid file]");
//                        MyAPIGateway.Utilities.ShowMessage("HAL", "/roid files");
//                        MyAPIGateway.Utilities.ShowMessage("HAL", "Example: /roid 0 0 0 asteroid0.vx2");
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

//            if (words.Count >= 3)
//            {
//                string roidFile = RandoRoid();
//                float x, y, z;
//                string xs = words[1];
//                string ys = words[2];
//                string zs = words[3];

//                if (words.Count > 4)
//                {
//                    roidFile = words[4];
//                    if (!roidFile.Contains(".vx2"))
//                    {
//                        roidFile += ".vx2";
//                    }
//                }

//                // thepenguinmaster is watching you... 
//                MyAPIGateway.Utilities.ShowMessage("HAL", "Attempting to create roid...");
//                if (Single.TryParse(xs, out x) && Single.TryParse(ys, out y) && Single.TryParse(zs, out z))
//                {
//                    // try to make the roid
//                    newRoid = PlaceRoid(roidFile, new MyPositionAndOrientation(new Vector3(x, y, z), Vector3.Up, Vector3.Up));

//                    // this can happen if the user specified a roid file name that is not in their save path. Append the voxel path to check there
//                    if (newRoid == null)
//                    {
//                        newRoid = PlaceRoid(VoxelFilePath + roidFile, new MyPositionAndOrientation(new Vector3(x, y, z), Vector3.Up, Vector3.Up));

//                        // ok their roid failed. they did somethign wrong
//                        if (newRoid == null)
//                        {
//                            MyAPIGateway.Utilities.ShowMessage("HAL", "Failed to create roid. Check roid file name if given");
//                            return;
//                        }
//                    }
//                    //newRoid.Storage.();
//                    //MyAPIGateway.Utilities.ShowMessage("HAL", "Roid created:" + newRoid.VoxelMapId + " Size:" + newRoid.Size.ToString());
//                }
//                else
//                {
//                    MyAPIGateway.Utilities.ShowMessage("HAL", "Sorry, that location is no good.");
//                }
//            }
//        }

//        Sandbox.ModAPI.IMyVoxelMap PlaceRoid(string roidFile, MyPositionAndOrientation position)
//        {
//            IMyVoxelMap createdVoxel = null;
//            Sandbox.Common.ObjectBuilders.Voxels.MyObjectBuilder_VoxelMap vMap;
            
//            vMap = new Sandbox.Common.ObjectBuilders.Voxels.MyObjectBuilder_VoxelMap { Filename = roidFile, PositionAndOrientation = position };

//            if (vMap != null)
//            {
//                vMap.PersistentFlags = MyPersistentEntityFlags2.InScene;
//                createdVoxel = (IMyVoxelMap)MyAPIGateway.Entities.CreateFromObjectBuilderAndAdd(vMap);
//                ((IMyEntity)createdVoxel).
//                //createdVoxel = (IMyVoxelMap)MyAPIGateway.Entities.CreateFromObjectBuilder
//            }

           

//            return createdVoxel;
//        }

//        string RandoRoid()
//        {
//            string retVal = "";
//            // select a rando from the enum
//            //VRage.MyRandom random = new VRage.MyRandom();

//            int enumLen = Enum.GetValues(typeof(Sandbox.Common.ObjectBuilders.Voxels.MyMwcVoxelFilesEnum)).Length;
//           // retVal = Enum.GetName(typeof(Sandbox.Common.ObjectBuilders.Voxels.MyMwcVoxelFilesEnum), random.Next(enumLen)) + ".vx2";
//            retVal = Enum.GetName(typeof(Sandbox.Common.ObjectBuilders.Voxels.MyMwcVoxelFilesEnum), 1) + ".vx2";
//            return VoxelFilePath + retVal;
//        }

//        Vector3 CreateRandomDirection()
//        {
//            Random rand = new Random();
//            return new Vector3(rand.Next(0, 100) / 100, rand.Next(0, 100) / 100, rand.Next(0, 100) / 100);
//        }

//        protected override void UnloadData()
//        {
//            MyAPIGateway.Utilities.MessageEntered -= Utilities_MessageEntered;
//            base.UnloadData();
//        }
//    }
//}
