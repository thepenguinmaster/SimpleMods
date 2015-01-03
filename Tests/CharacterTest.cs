using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces;
using Sandbox.ModAPI.Ingame;
using Sandbox.Game;

using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.Components;
using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using System.IO;
using VRageMath;


namespace scripts.ThePenuginMaster
{
    [Sandbox.Common.MySessionComponentDescriptor(Sandbox.Common.MyUpdateOrder.BeforeSimulation)]
    class CharacterTest : Sandbox.Common.MySessionComponentBase
    {
        private bool _IsLoaded = false;
        //VRage.Collections.DictionaryReader<long, MyPlayerInfo> lastinfo = new VRage.Collections.DictionaryReader<long, MyPlayerInfo>();

        public override void UpdateBeforeSimulation()
        {
            //VRage.Collections.DictionaryReader<long,MyPlayerInfo> lastinfo = new VRage.Collections.DictionaryReader<long,MyPlayerInfo>();
            //MyAPIGateway.Multiplayer.Players.AllPlayers[0].IsDead

            if (!_IsLoaded)
            {
                //init();
            }


        }


        //public void init()
        //{
        //    _IsLoaded = true;
        //    MyAPIGateway.Utilities.MessageEntered += Utilities_MessageEntered;
        //}

        //private void Utilities_MessageEntered(string messageText, ref bool sendToOthers)
        //{
        //    if (!messageText.StartsWith("/")) { return; }
        //    List<string> words = new List<string>(messageText.Trim().ToLower().Replace("/", "").Split(' '));
        //    sendToOthers = false;
        //    switch (words[0])
        //    {
        //        case "report":
        //            MyAPIGateway.Utilities.ShowMessage("HAL", "Mod Loaded: CharacterTests");
        //            break;
        //        case "getcharacter":
        //            getCharacterTest();
        //            break;
        //        case "voxel":
        //            testVoxel();
        //            break;
        //    }
        //}

        //void testVoxel()
        //{
        //    WriteToFile("testing voxel..");
        //        WriteToFile("keycount "+Sandbox.ModAPI.MyAPIGateway.Session.VoxelMaps.GetVoxelMapsArray().Keys.Count);
        //    foreach (string s in Sandbox.ModAPI.MyAPIGateway.Session.VoxelMaps.GetVoxelMapsArray().Keys)
        //    {
        //        WriteToFile(s);
        //    }

        //    byte[] voxelArray = Sandbox.ModAPI.MyAPIGateway.Session.VoxelMaps.GetVoxelMapsArray()["Small_Pirate_Base_3_1.vx2"];

        //    foreach (byte b in voxelArray)
        //    {
        //        WriteToFile(string.Format("{0:x2} ", b));
        //    }
            


        //    //Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_Definitions.VoxelMaterials
        //}
        //void getCharacterTest()
        //{
        //    HashSet<IMyEntity> m_entitiesCache = new HashSet<IMyEntity>();
        //    WriteToFile("starting");

        //    //IMyEntity player = Sandbox.ModAPI.MyAPIGateway.Entities.GetEntityByName("thepenguinmaster");
        //    //if (player != null)
        //    //{
        //    //    WriteToFile("Player entity location " + player.GetPosition().X);
        //    //}

        //    var p = MyAPIGateway.Entities.GetEntity(x => x.DisplayName == "thepenguinmaster") as IMyEntity;
        //    if (p != null)
        //    {
        //        WriteToFile("Player entity location " + p.GetPosition().X);
        //        MyObjectBuilder_Character cob = (Sandbox.Common.ObjectBuilders.MyObjectBuilder_Character)p;
        //        if (cob != null)
        //        {
        //            WriteToFile(cob.Health.ToString());
        //        }
        //    }

            //MyObjectBuilder_Character chr2 = Sandbox.ModAPI.MyAPIGateway.Session.Player.PlayerCharacter.Entity as MyObjectBuilder_Character;

            //if (chr2 != null)
            //{
            //    WriteToFile(chr2.Health.ToString());
            //}

            //MyAPIGateway.Entities.GetEntities(m_entitiesCache, x => x.Name == x.Name);
            //foreach (IMyEntity e in m_entitiesCache)
            //{
            //    WriteToFile("name " + e.Name);
            //    WriteToFile("friendly name " + e.GetFriendlyName());
            //    WriteToFile("displ name" + e.DisplayName);
            //}

            //    //Sandbox.Common.ObjectBuilders.MyObjectBuilder_Character.Health
            //    //WriteToFile();
            //    //WriteToFile();
            //}

            //foreach ( MyObjectBuilder_EntityBase d in Sandbox.ModAPI.MyAPIGateway.Session.GetSector().SectorObjects)
            //{
            //    WriteToFile("entitybase " + d.Name );
            //    WriteToFile("sub " + d.SubtypeId);
            //    WriteToFile("sub n " + d.SubtypeName);
            //    WriteToFile("tid " + d.TypeId);

            //}

            //Sandbox.ModAPI.MyAPIGateway.Players[0].Controller.
        //}


        //private void WriteToFile(string text)
        //{
        //    string path = @"c:\test.txt";

        //    using (StreamWriter sw = File.AppendText(path))
        //    {
        //        sw.WriteLine(text);
        //    }
        //}


        protected override void UnloadData()
        {
            //MyAPIGateway.Utilities.MessageEntered -= Utilities_MessageEntered;
            base.UnloadData();
        }


    }
}
