﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using OxyPlot;
using OxyPlot.Series;

namespace kinematics_20160720
{
    class joint_chart_pannel_cls
    {
        private int FRAME_PERIOD = 24;

        private timeline_chart_cls timeline_chart;
        private cycle_chart_cls axis_angle_chart, frontal_chart, sagittal_chart, horizontal_chart;

        //private OxyPlot.Wpf.PlotView plotview;
        private PlotModel plotmodel;

        // debug 
        int counter = 0;
        Boolean do_job = true;

        // public constructor
        public joint_chart_pannel_cls(OxyPlot.Wpf.PlotView main_timeline_plotview, OxyPlot.Wpf.PlotView main_joint_angle_plot_view,
                                      OxyPlot.Wpf.PlotView frontal_projection_plot_view, OxyPlot.Wpf.PlotView sagittal_projection_plot_view, 
                                      OxyPlot.Wpf.PlotView horizontal_projection_plot_view) 
        {
            main_timeline_plotview.Model = new PlotModel();
            OxyPlot.Axes.LinearAxis time_axis = new OxyPlot.Axes.LinearAxis();
            OxyPlot.Axes.LinearAxis angle_axis = new OxyPlot.Axes.LinearAxis();
            time_axis.Position = OxyPlot.Axes.AxisPosition.Bottom;
            angle_axis.Position = OxyPlot.Axes.AxisPosition.Left;
            time_axis.Minimum = 0;
            time_axis.Maximum = 10;
            angle_axis.Minimum = 0;
            angle_axis.Maximum = 180;
            main_timeline_plotview.Model.Axes.Add(time_axis);
            main_timeline_plotview.Model.Axes.Add(angle_axis);
            LineSeries series = new LineSeries();
            main_timeline_plotview.Model.Series.Add(series);

            axis_angle_chart = new cycle_chart_cls(main_joint_angle_plot_view, "Угол между осями");
            axis_angle_chart.set_time_axis(0, 10);
            axis_angle_chart.set_angle_axis(0, 180);
            frontal_chart = new cycle_chart_cls(frontal_projection_plot_view, "Фронтальная проекция");
            frontal_chart.set_time_axis(0, 10);
            frontal_chart.set_angle_axis(-90, 90);
            sagittal_chart = new cycle_chart_cls(sagittal_projection_plot_view, "Сагиттальная проекция");
            sagittal_chart.set_time_axis(0, 10);
            sagittal_chart.set_angle_axis(-90, 90);
            horizontal_chart = new cycle_chart_cls(horizontal_projection_plot_view, "Горизонтальная проекция");
            horizontal_chart.set_time_axis(0, 10);
            horizontal_chart.set_angle_axis(-90, 90);
        }

        EventArgs e;
        public event EventHandler chart_update_tick;
        public void chart_update_thread_method()
        {
            while (do_job)
            {
                // every ... mSec request current joint data and display them
                Thread.Sleep(FRAME_PERIOD);

                // request data
                //Event will be null if there are no subscribers
                if (chart_update_tick != null)
                    chart_update_tick(this, e);

                // draw data *****************************************************************
                counter++;
            }
        }
    }
}
