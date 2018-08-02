﻿using System;
using OpenPager.Helper;
using OpenPager.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OpenPager.ViewModels
{
    public class OperationDetailViewModel : BaseViewModel
    {
        #region Properties
        public Operation Operation { get; set; }

        public Command AcceptCommand { get; set; }

        bool _isButtonVisible = false;
        public bool IsButtonVisible
        {
            get => _isButtonVisible;
            set => SetProperty(ref _isButtonVisible, value);
        }

        string _timer = "";
        public string Timer
        {
            get => _timer;
            set => SetProperty(ref _timer, value);
        }

        Color _color = Color.Black;
        public Color TimerColor
        {

            get => _color;
            set => SetProperty(ref _color, value);
        }
        #endregion

        public OperationDetailViewModel(Operation operation, bool isAlarm = false)
        {

            if (operation == null)
            {
                return;
            }

            IsButtonVisible = isAlarm;
            Operation = operation;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                TimeSpan diff = DateTime.Now - operation.Time;
                Timer = diff.ToString(@"hh\:mm\:ss");
                return true;
            });
            AcceptCommand = new Command(action =>
            {
                IsButtonVisible = false;
                TimerColor = Color.Black;
                Vibration.Cancel();
            });

            if (isAlarm)
            {
                TimerColor = Color.Red;
                AlarmHelper.Vibrate();
            }
        }
    }
}