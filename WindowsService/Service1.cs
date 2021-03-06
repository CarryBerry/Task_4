﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task_4.BusinessLayer;

namespace WindowsService
{
    public partial class Service1 : ServiceBase
    {
        Task_4.BusinessLayer.Monitor logger;
        public Service1()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            System.Diagnostics.Debugger.Launch();
            logger = new Task_4.BusinessLayer.Monitor();
            Thread loggerThread = new Thread(logger.Start);
            loggerThread.Start();
        }

        protected override void OnStop()
        {
            logger.Stop();
            Thread.Sleep(1000);
        }
    }
}