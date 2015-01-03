using Sandbox.ModAPI;
using Sandbox.ModAPI.Interfaces;
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
using System.Runtime.InteropServices;
namespace StarGate
{
    // tag based plugin

    // the user will make their structure, and tag something with a custom name
    // in the command they will do a quantum link between two gates
    // when the user approaches the gate, it will detect the location and place them to the matching gate

    // settings will be saved and loaded from local storage 
    // will be a pipe del;imated file where there is a set number of columns. if a value is not used, it is blank
    // values to start will be: SrcGate | DestGate | GateCoolDown | enabled. ... etc..


    [Sandbox.Common.MySessionComponentDescriptor(Sandbox.Common.MyUpdateOrder.BeforeSimulation)]
    class StarGate : Sandbox.Common.MySessionComponentBase
    {

        private bool _IsLoaded = false;
        private List<SGEntry> StarGates = new List<SGEntry>();

        public override void UpdateBeforeSimulation()
        {
            if (!_IsLoaded)
            {
                init();
            }

            if (_IsLoaded)
            {
                PruneDeadGates();
                CheckStargatesForActivation();
            }
        }

        public void init()
        {

            LoadGateFile();
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
                case "sg":
                    ProcessSG(words);
                    break;
            }
        }

        void ProcessSG(List<string> words)
        {
            switch (words[1])
            {
                case "help":
                    break;

                case "create":
                    if (words.Count >= 3)
                    {
                        CreateSG(words);
                    }
                    break;

                case "remove":
                    if (words.Count >= 2)
                    {
                        RemoveSG(words);
                    }
                    break;

                case "query":
                    queryStagGates();
                    break;
            }
        }

        void queryStagGates()
        {
            if (StarGates != null)
            {
                Sandbox.ModAPI.MyAPIGateway.Utilities.ShowMessage("HAL", "Current StarGates:");
                foreach (SGEntry entry in StarGates){
                    Sandbox.ModAPI.MyAPIGateway.Utilities.ShowMessage("HAL",string.Format("{0} -> {1}", entry.SrcGate.DisplayName, entry.DestGate.DisplayName ));
                }
            }
        }

        void RemoveSG(List<string> words)
        {
            string srcName = "";

            srcName = words[2];

            // find the source name in the lsit and remove it
            List<SGEntry> tmpList = new List<SGEntry>(StarGates.ToArray());


            foreach (SGEntry entry in StarGates)
            {
                if (entry.SrcGate.DisplayName == srcName)
                {
                    tmpList.Remove(entry);
                }
            }

            StarGates = tmpList;

            SaveGateFile();
            Sandbox.ModAPI.MyAPIGateway.Utilities.ShowMessage("HAL", "Warp gate decommissioned");
        }

        void CreateSG(List<string> words)
        {
            // create [blockname] [destblockname] [o:cooldown] 
            string srcName = "";
            string dstName = "";
            int coolDown = 0;
            IMyEntity srcEntity;
            IMyEntity dstEntity;
            SGEntry newStargete;

            try
            {
                //sg create s1 s2 0
                srcName = words[2];
                dstName = words[3];
                coolDown = 0;
                if (words.Count > 4)
                {
                    int.TryParse(words[4].ToString(), out coolDown);
                }
            }
            catch
            {
                Sandbox.ModAPI.MyAPIGateway.Utilities.ShowMessage("HAL", "Invalid input. Please try again");
                return;
            }

            // search for src and dest entities
            HashSet<IMyEntity> _StarGateEntity = new HashSet<IMyEntity>();
            MyAPIGateway.Entities.GetEntities(_StarGateEntity, ent => ent is IMyCubeGrid);

            srcEntity = MyAPIGateway.Entities.GetEntity(ent => ent.DisplayName == srcName);
            dstEntity = MyAPIGateway.Entities.GetEntity(ent => ent.DisplayName == dstName);
            Sandbox.ModAPI.MyAPIGateway.Utilities.ShowMessage("HAL", "Quantum pair established");
            if (srcEntity == null || dstEntity == null)
            {
                Sandbox.ModAPI.MyAPIGateway.Utilities.ShowMessage("HAL", "Sorry, but I could not find one of the gates. Check the names and try again.");
                return;
            }

            // if they exist continue
            newStargete = new SGEntry
            {
                SrcGate = srcEntity,
                CoolDown = coolDown,
                DestGate = dstEntity,
                IsActive = true,
                ActiveRange = 20,
                SpawnOffset = new Vector3(40, 0, 0),
                WarningDist = 50
            };

            // add the sg entry to the dictionary
            StarGates.Add(newStargete);
            Sandbox.ModAPI.MyAPIGateway.Utilities.ShowMessage("HAL", "Singularity drive on-line");
            // save the dictionary
            SaveGateFile();
            Sandbox.ModAPI.MyAPIGateway.Utilities.ShowMessage("HAL", "Warp gate operational");
        }

        void GetStarGates()
        {
            //MyAPIGateway.Entities.GetEntities(_StarGates, (x) => x is IMyCubeGrid && m_enemies.ContainsKey(x.DisplayName));
            //    foreach (var ent in m_entitiesCache)
            //        m_data.EnemyShips.Add(new EnemyShip(ent as IMyCubeGrid, m_enemies[ent.DisplayName]));
            //    m_entitiesCache.Clear();
        }

        void LoadGateFile()
        {
            IMyEntity srcEntity;
            IMyEntity dstEntity;
            int coolDown = 0;
            float activeRange = 20;
            float warningDist = 50;
            bool isActive = true;

            Vector3 spawnOffset = new Vector3(warningDist + 10, 0, 0);

            //// values to start will be: SrcGate | DestGate | GateCoolDown | enabled. ... etc..
            try
            {
                TextReader reader = MyAPIGateway.Utilities.ReadFileInLocalStorage("StarGates_" + Sandbox.ModAPI.MyAPIGateway.Session.WorldID, typeof(StarGate));
                if (reader != null)
                {
                    string fileInput = reader.ReadToEnd();
                    // now split it out using the crlf
                    string[] gateEntries = fileInput.Trim().Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                    foreach (string gate in gateEntries)
                    {
                        // split by pipe and add to our dictionary, ensure empties are there
                        string[] gateProps = gate.Split(new string[] { "|" }, StringSplitOptions.None);
                        string srcName = gateProps[0];
                        string dstName = gateProps[1];

                        if (srcName == null || srcName == "" || dstName == null || dstName == "") continue;
                        srcEntity = MyAPIGateway.Entities.GetEntity(ent => ent.DisplayName == srcName);
                        dstEntity = MyAPIGateway.Entities.GetEntity(ent => ent.DisplayName == dstName);
                        try
                        {
                            int.TryParse(gateProps[2], out coolDown);
                            bool.TryParse(gateProps[3], out isActive);

                            Single.TryParse(gateProps[4], out activeRange);
                            Single.TryParse(gateProps[5], out warningDist);

                            Single.TryParse(gateProps[6], out spawnOffset.X);
                            Single.TryParse(gateProps[7], out spawnOffset.Y);
                            Single.TryParse(gateProps[8], out spawnOffset.Z);
                        }
                        catch { }

                        SGEntry newStargete = new SGEntry
                        {
                            SrcGate = srcEntity,
                            DestGate = dstEntity,
                            CoolDown = coolDown,
                            IsActive = isActive,
                            ActiveRange = activeRange,
                            SpawnOffset = spawnOffset,
                            WarningDist = warningDist
                        };
                        StarGates.Add(newStargete);
                    }
                }
            }
            catch
            {
                //Sandbox.ModAPI.MyAPIGateway.Utilities.ShowMessage("HAL", "Error loading quantum pairs. Pairs have destabilized. Unknown anomaly");
            }
        }

        void SaveGateFile()
        {
            //// values to start will be: SrcGate | DestGate | GateCoolDown | enabled... etc../
            // save the gates for the future
            using (TextWriter writer = MyAPIGateway.Utilities.WriteFileInLocalStorage("StarGates_"+ Sandbox.ModAPI.MyAPIGateway.Session.WorldID , typeof(StarGate)))//
            {
                string outString = "";
                if (writer != null)
                {
                    foreach (SGEntry gate in StarGates)
                    {
                        outString += string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}", gate.SrcGate.DisplayName, gate.DestGate.DisplayName, gate.CoolDown, gate.IsActive, gate.ActiveRange, gate.WarningDist, gate.SpawnOffset.X, gate.SpawnOffset.Y, gate.SpawnOffset.Z);
                        outString += "\r\n"; //Environment.NewLine;
                    }
                    writer.Write(outString);
                }
                else
                {
                    Sandbox.ModAPI.MyAPIGateway.Utilities.ShowMessage("HAL", "Error saving quantum pairs. Pairs may destabilize after universal reload.");
                }
            }
        }

        void CheckStargatesForActivation()
        {
            // loop through the entities in the stargate entity list and check if a player is close if it is then do the dew
            IMyEntity possiblePlayer;
            log("checking for activation");
            if (StarGates == null) return;
            try
            {
                foreach (SGEntry gate in StarGates)
                {
                    if (gate.IsActive)
                    {
                        var warningSphere = new BoundingSphereD(gate.SrcGate.GetPosition(), gate.WarningDist);
                        var warpSphere = new BoundingSphereD(gate.SrcGate.GetPosition(), gate.ActiveRange);

                        possiblePlayer = GetPlayerInSphere(ref warningSphere);
                        if (possiblePlayer != null)
                        {
                            // if it is your player
                            if (possiblePlayer.DisplayName == Sandbox.ModAPI.MyAPIGateway.Session.Player.DisplayName)
                            {
                                // warning
                                //Sandbox.ModAPI.MyAPIGateway.Utilities.ShowNotification("You are approaching a Star Gate. If you proceed you will be transported.");
                            }
                        }

                        // process closer people and transport

                        possiblePlayer = GetPlayerInSphere(ref warpSphere);
                        if (possiblePlayer != null)
                        {
                            // Get the remote gate for the gate that is being approached
                            Sandbox.ModAPI.MyAPIGateway.Utilities.ShowNotification("Locking on to remote gate.");
                            Vector3 DestLocation = gate.DestGate.GetPosition();
                            DestLocation.X += gate.SpawnOffset.X;
                            DestLocation.Y += gate.SpawnOffset.Y;
                            DestLocation.Z += gate.SpawnOffset.Z;

                            // punch it chewy!
                            //arwwwwwaaaaaaaarrrrrrahahahahahhaahhhhaa
                            if (possiblePlayer.Parent != null)
                            {
                                IMyEntity entityToMove = possiblePlayer.GetTopMostParent();
                                entityToMove.SetPosition(DestLocation);
                            }
                            else
                            {
                                possiblePlayer.SetPosition(DestLocation);
                            }
                        }
                    }
                }
            }
            catch { log("SOme error in check activated"); }
        }

        void PruneDeadGates()
        {

            // remove/disables all dead gates
            // this is any gate that has been deleted
            IMyEntity srcEntity;
            IMyEntity dstEntity;

            if (StarGates == null) return;
            try
            {
                // maybe even check if the gate has power or is excessively damaged
                foreach (SGEntry entry in StarGates)
                {
                    // look for the src and dest. If they are gone we can either disable or remove.
                    srcEntity = MyAPIGateway.Entities.GetEntity(ent => ent.DisplayName == entry.SrcGate.DisplayName);
                    dstEntity = MyAPIGateway.Entities.GetEntity(ent => ent.DisplayName == entry.DestGate.DisplayName);

                    // if it is not here, then remove it. It could have been killed by a player etc..
                    if (srcEntity == null || dstEntity == null)
                    {
                        // let users know
                        if (srcEntity == null)
                        {
                            Sandbox.ModAPI.MyAPIGateway.Utilities.ShowNotification(string.Format("StarGate {0} singularity drive has destabilized. Quantum pair removed.", srcEntity.DisplayName));
                        }
                        if (dstEntity == null)
                        {
                            Sandbox.ModAPI.MyAPIGateway.Utilities.ShowNotification(string.Format("StarGate destination {0} singularity drive has destabilized. Quantum pair removed.", dstEntity.DisplayName ));
                        }

                        // remove the entry
                        StarGates.Remove(entry);

                        // save
                        SaveGateFile();
                    }
                }
            }
            catch { log("error"); }
        }

        // check for mp later
        public IMyEntity GetPlayerInSphere(ref BoundingSphereD sphere)
        {
            var ents = MyAPIGateway.Entities.GetEntitiesInSphere(ref sphere);
            foreach (var entity in ents)
            {
                var controllable = entity as IMyControllableEntity;
                if (controllable != null && (MyAPIGateway.Multiplayer.Players.GetPlayerControllingEntity(entity) != null || controllable == MyAPIGateway.Session.ControlledObject))
                    return entity;
            }
            return null;
        }

        protected override void UnloadData()
        {
            MyAPIGateway.Utilities.MessageEntered -= Utilities_MessageEntered;
            base.UnloadData();
        }

        public class SGEntry
        {
            public IMyEntity SrcGate;
            public IMyEntity DestGate;
            public int CoolDown;
            public bool IsActive;
            public float ActiveRange;
            public float WarningDist;
            public Vector3 SpawnOffset;
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
