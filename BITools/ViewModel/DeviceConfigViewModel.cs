using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NotifyBase;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using BITools.SystemManager;
using System.Collections.ObjectModel;
using BIDataAccess.entities;
using Newtonsoft.Json;
using BIModel;
using BILogic;
using BITools.ViewModel.Configs;
using BITools.Core;

namespace BITools.ViewModel
{
    class DeviceConfigViewModel : BaseViewModel
    {
        private DeviceConfigService service;
        public DeviceConfigViewModel()
        {
            TCList = new ObservableCollection<Configs.TCViewModel>();
            service = new DeviceConfigService();
        }

        public ICommand LoadConfigCommand { get { return new DelegateCommand(LoadConfig); } }
        public ICommand SaveConfigCommand { get { return new DelegateCommand(SaveConfig); } }


        public ICommand LoadFileCommand { get { return new DelegateCommand(LoadFile); } }
        public ICommand SaveFileCommand { get { return new DelegateCommand(SaveFile); } }
        public ICommand SaveAsFileCommand { get { return new DelegateCommand(SaveAsFile); } }


        public ICommand ClearConfigCommand { get { return new DelegateCommand(ClarConfig); } }


        public ICommand AddTCCommand { get { return new DelegateCommand(AddTC); } }
        public ICommand ModifyTCCommand { get { return new DelegateCommand(ModifyTC); } }

        public ICommand AddLayerCommand { get { return new DelegateCommand<Configs.TCViewModel>(AddLayer); } }
        public ICommand ModifyLayerCommand { get { return new DelegateCommand(ModifyLayer); } }

        public ICommand AddUUTCommand { get { return new DelegateCommand<Configs.LayerViewModel>(AddUUT); } }
        public ICommand ModifyUUTCommand { get { return new DelegateCommand(ModifyUUT); } }

        public ICommand AddChannelCommand { get { return new DelegateCommand<Configs.UUTViewModel>(AddChannel); } }
        public ICommand ModifyChannelCommand { get { return new DelegateCommand(ModifyChannel); } }
        //public ICommand ChannelSelectItemCommand { get { return new DelegateCommand<ChannelViewModel>(ModifyChannel); } }

        public ObservableCollection<TCViewModel> TCList
        {
            get { return this.GetValue(c => c.TCList); }
            set { this.SetValue(c => c.TCList, value); }
        }

        private DeviceConfig currentConfig;

        private void LoadConfig()
        {
            var window = new DeviceListWindow();
            var dialog = window.ShowDialog().GetValueOrDefault();
            if (dialog)
            {
                currentConfig = window.DeviceConfig;
                TCList = JsonConvert.DeserializeObject<ObservableCollection<TCViewModel>>(currentConfig.DeviceContent);
            }
        }

        private void SaveConfig()
        {
            if (this.TCList == null || this.TCList.Count == 0)
                return;

            if (currentConfig == null)
            {
                var window = new ConfigNameWindow();
                var dialog = window.ShowDialog().GetValueOrDefault();
                if (dialog)
                {
                    var content = JsonConvert.SerializeObject(this.TCList);
                    DeviceConfig entity = new DeviceConfig();
                    entity.Name = window.ConfigName;
                    entity.IsDefault = window.IsDefault;
                    entity.DeviceContent = content;
                    entity.User = AppContext.UserName;
                    entity.CreateTime = DateTime.Now;
                    service.CreateConfig(entity);
                    MsgBox.SuccessShow("保存成功！");
                }
            }
            else
            {
                var content = JsonConvert.SerializeObject(this.TCList);
                currentConfig.DeviceContent = content;
                service.UpdateConfig(currentConfig);
                MsgBox.SuccessShow("保存成功！");
            }
        }

        string filePath = "";
        private void LoadFile()
        {
            filePath = FileManager.OpenParamFile();
            if (filePath.IsEmpty())
                return;

            var content = System.IO.File.ReadAllText(filePath);
            TCList = JsonConvert.DeserializeObject<ObservableCollection<TCViewModel>>(content);
        }

        private void SaveFile()
        {
            if (this.TCList == null || this.TCList.Count == 0)
                return;

            if (filePath.IsEmpty())
            {
                filePath = FileManager.SaveParamFile();
                if (filePath.IsEmpty())
                    return;
            }

            var content = JsonConvert.SerializeObject(this.TCList);
            content = FunExt.JsonFormatter(content);

            System.IO.File.WriteAllText(filePath, content);
            MsgBox.SuccessShow("保存成功！");
        }

        private void SaveAsFile()
        {
            if (this.TCList == null || this.TCList.Count == 0)
                return;

            var filepath = FileManager.SaveParamFile();
            if (filepath.IsEmpty())
                return;

            var content = JsonConvert.SerializeObject(this.TCList);
            content = FunExt.JsonFormatter(content);

            System.IO.File.WriteAllText(filepath, content);
            MsgBox.SuccessShow("保存成功！");
        }

        private void ClarConfig()
        {
            var dialog = MsgBox.QuestionShow("确认清空当前配置吗？");
            if (dialog == MsgBoxResult.OK)
            {
                this.TCList.Clear();
            }
        }

        private void AddTC()
        {
            var window = new TCConfigWindow();
            var dialog = window.ShowDialog();
            if (dialog.GetValueOrDefault())
            {
                TCList.Add(window.TCViewModel);
            }
        }

        private void ModifyTC()
        {

        }

        private void AddLayer(TCViewModel tc)
        {
            if (tc == null)
                return;

            var window = new LayerConfigWindow();
            var dialog = window.ShowDialog();
            if (dialog.GetValueOrDefault())
            {
                var max = window.MaxLayer.ToInt32();
                if (max > 0)
                {
                    if (window.IsAllTC)
                    {
                        foreach (var item in TCList)
                        {
                            item.LayerList.Clear();
                            for (int i = 1; i <= max; i++)
                            {
                                item.LayerList.Add(new Configs.LayerViewModel { Code = "" + i, Name = "L" + i });
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= max; i++)
                        {
                            tc.LayerList.Add(new Configs.LayerViewModel { Code = "" + i, Name = "L" + i });
                        }
                    }
                }
                else
                {
                    tc.LayerList.Add(window.LayerViewModel);
                }
            }
        }

        private void ModifyLayer()
        {

        }

        private TCViewModel getTC(Configs.LayerViewModel model)
        {
            TCViewModel ok = null;
            foreach (var tc in TCList)
            {
                foreach (var layer in tc.LayerList)
                {
                    if (layer.Code == model.Code)
                    {
                        ok = tc;
                    }
                }
            }
            return ok;
        }

        private void AddUUT(Configs.LayerViewModel layerSource)
        {
            if (layerSource == null)
                return;

            var window = new UUTConfigWindow();
            var dialog = window.ShowDialog();
            if (dialog.GetValueOrDefault())
            {
                if (window.IsAllTC && window.IsAllLayer)
                {
                    int max = window.MaxCode.ToInt32();
                    foreach (var tc in TCList)
                    {
                        foreach (var layer in tc.LayerList)
                        {
                            layer.UUTList.Clear();
                            for (int i = 1; i <= max; i++)
                            {
                                var uut = new UUTViewModel { Code = i.ToString() };
                                uut.Init();
                                layer.UUTList.Add(uut);
                            }
                        }
                    }
                }
                else if (window.IsAllTC == false && window.IsAllLayer)
                {
                    var tc = getTC(layerSource);
                    int max = window.MaxCode.ToInt32();
                    if (max > 0)
                    {
                        foreach (var layer in tc.LayerList)
                        {
                            layer.UUTList.Clear();
                            for (int i = 1; i <= max; i++)
                            {
                                var uut = new UUTViewModel { Code = i.ToString() };
                                uut.Init();
                                layer.UUTList.Add(uut);
                            }
                        }
                    }
                }
                else
                {
                    int max = window.MaxCode.ToInt32();
                    if (max > 0)
                    {
                        for (int i = 1; i <= max; i++)
                        {
                            var uut = new UUTViewModel { Code = i.ToString() };
                            uut.Init();
                            layerSource.UUTList.Add(uut);
                        }
                    }
                    else
                    {
                        layerSource.UUTList.Add(window.UUTViewModel);
                    }
                }
            }
        }

        private void ModifyUUT()
        {

        }

        private void AddChannel(Configs.UUTViewModel uut)
        {
            //var window = new ChannelConfigWindow(null);
            //var dialog = window.ShowDialog();
            //if (dialog.GetValueOrDefault())
            //{
            //    uut.ChannelList.Add(window.ChannelInterfaceViewModel);
            //}
        }

        private void ModifyChannel()
        {

        }
    }
}
