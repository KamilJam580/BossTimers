using System;
using System.Collections.Generic;
using System.Text;
using TestAPp.ViewModels;

namespace TestAPp.Services
{
    public class BossPageSelected
    {
        private BossPageSelected() { }
        private static BossPageSelected _instance;
        public static BossPageSelected GetInstance()
        {
            if (_instance == null)
            {
                _instance = new BossPageSelected();
            }
            return _instance;
        }
        public BossesViewModel viewModel;
        public string dbPath;
    }
}
