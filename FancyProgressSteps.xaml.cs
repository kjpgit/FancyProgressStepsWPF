using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;


namespace kjp;


public partial class FancyProgressSteps : UserControl
{
    public ObservableCollection<FancyStep> Steps { get; set; } = new();

    private int currentIndex = -1;

    public FancyProgressSteps()
    {
        InitializeComponent();
        MyItems.ItemsSource = Steps;

        // Put your steps here
        Steps.Add(new FancyStep("Detect"));
        Steps.Add(new FancyStep("Set PIN"));
        Steps.Add(new FancyStep("Unplug\nand unlock"));
        Steps.Add(new FancyStep("Run\nUnlocker"));
        Steps.Add(new FancyStep("Format"));
        Steps.Add(new FancyStep("Finished"));

        SetIndex(2);  // For previewing in the xaml designer
    }

    public void SetIndex(int idx)
    {
        this.currentIndex = idx;
        Refresh();
    }

    public void Bump()
    {
        currentIndex++;
        if (currentIndex >= Steps.Count) {
            currentIndex = 0;
        }
        Refresh();
    }

    private void Refresh()
    {
        for (int i = 0; i < Steps.Count; i++) {
            var step = Steps[i];
            var isLast = (i == Steps.Count - 1);
            if (i < currentIndex) {
                step.LeftPath = "DONE";
                step.RightPath = "DONE";
                step.Node = "DONE";
            }
            if (i == currentIndex) {
                step.LeftPath = "DONE";
                step.RightPath = "TODO";
                step.Node = "ACTIVE";
            }
            if (i > currentIndex) {
                step.LeftPath = "TODO";
                step.RightPath = "TODO";
                step.Node = "TODO";
            }
            if (i == 0) {
                step.LeftPath = "";
            }
            if (isLast) {
                step.RightPath = "";
            }
            step.Refresh();
        }
    }
}


public class FancyStep : INotifyPropertyChanged
{
    public FancyStep(string name)
    {
        Name = name;
    }
    public string Name { get; set; }
    public string LeftPath { get; set; } = "";
    public string RightPath { get; set; } = "";
    public string Node { get; set; } = "";

    public void Refresh()
    {
        OnPropertyChanged(nameof(Node));
        OnPropertyChanged(nameof(LeftPath));
        OnPropertyChanged(nameof(RightPath));
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}