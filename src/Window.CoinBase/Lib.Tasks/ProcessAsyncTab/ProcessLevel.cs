using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Lib.Data.Repository.Tasks;
using Lib.Domain.AsynTabs;


namespace Lib.Tasks.ProcessAsyncTab
{
    public class ProcessLevel : ITask
    {
        public ProcessLevel()
        {
        }
        public void Execute()
        {
            BonusBuild();
        }

        public void BonusBuild()
        {
            var _task = new TaskRepository();
            var data = _task.AsynTab_Get((int)AsynTabType.PROCESS_VOLUME_SYSTEM, (int)AsynTabStatus.COMPLETED);
            foreach (AsynTab it in data)
            {
                try
                {
                    _task.AsynTab_Update(it.Id, (int)AsynTabStatus.PROCESS_LEVEL);
                    BonusLevelExtraData extraData = JsonConvert.DeserializeObject<BonusLevelExtraData>(it.ExtraData);
                    int investUsd = (int)extraData.AmountUSD;
                    var referalData = _task.Muser_Get_Referal_Id(it.UserId);
                    if (referalData.Count() == 0)
                    {
                        _task.AsynTab_Update(it.Id, (int)AsynTabStatus.COMPLETED_LEVEL);
                        continue;
                    }

                    string parrentIds = string.Join(",", referalData.Select(x => x.ParentId).ToList());

                    var ProData = _task.Get_Process_Level_data(parrentIds);
                    foreach(ProcessLevelData pro in ProData)
                    {
                        try
                        {
                            ProcessData(pro, _task);
                        }
                        catch
                        { }
                    }
                    _task.AsynTab_Update(it.Id, (int)AsynTabStatus.COMPLETED_LEVEL);
                }
                catch
                {
                    _task.AsynTab_Update(it.Id, (int)AsynTabStatus.FAIL_LEVEL);
                }
            }
        }

        public void ProcessData(ProcessLevelData levelData, TaskRepository task)
        {
            int level = 0;
            double masterIB = 0;
            if(levelData.TotalF1 >= 7 && levelData.TotalF1 < 15)
            {
                if(levelData.TotalVolumn >= 100000)
                {
                    level = 2;
                    masterIB = 0.6;
                }
            }
            else if (levelData.TotalF1 >= 15 && levelData.TotalF1 < 25)
            {
                if (levelData.TotalVolumn >= 200000)
                {
                    level = 3;
                    masterIB = 0.8;
                }
                else if (levelData.TotalVolumn >= 100000)
                {
                    level = 2;
                    masterIB = 0.6;
                }
            }
            else if (levelData.TotalF1 >= 25 && levelData.TotalF1 < 35)
            {
                if (levelData.TotalVolumn >= 400000)
                {
                    level = 4;
                    masterIB = 1;
                }
                else if (levelData.TotalVolumn >= 200000 && levelData.TotalVolumn < 400000)
                {
                    level = 3;
                    masterIB = 0.8;
                }
                else if (levelData.TotalVolumn >= 100000)
                {
                    level = 2;
                    masterIB = 0.6;
                }
            }
            else if (levelData.TotalF1 >= 35)
            {
                if (levelData.TotalVolumn >= 700000)
                {
                    level = 5;
                    masterIB = 1.2;
                }
                else if (levelData.TotalVolumn >= 400000 && levelData.TotalVolumn < 700000)
                {
                    level = 4;
                    masterIB = 1;
                }
                else if (levelData.TotalVolumn >= 200000 && levelData.TotalVolumn < 400000)
                {
                    level = 3;
                    masterIB = 0.8;
                }
                else if (levelData.TotalVolumn >= 100000)
                {
                    level = 2;
                    masterIB = 0.6;
                }
            }
            
            if(level > 0)
            {
                task.Update_Process_Level_data(levelData.UserId, level, (decimal)masterIB);
            }
        }
    }
}
