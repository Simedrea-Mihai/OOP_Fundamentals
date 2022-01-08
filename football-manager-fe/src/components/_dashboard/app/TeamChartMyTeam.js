import { merge } from 'lodash';
import ReactApexChart from 'react-apexcharts';
import axios from 'axios';
import { useEffect, useState } from 'react';
// material
import { Card, CardHeader, Box } from '@mui/material';
//
import { BaseOptionChart } from '../../charts';

export default function TeamChartMyTeam({ data }) {
  const CHART_DATA_MOCK = [
    {
      name: 'OVR',
      type: 'area',
      data: [2, 2, 2, 2, 2]
    },
    {
      name: 'Potential',
      type: 'line',
      data: [1, 1, 1, 1, 1]
    }
  ];

  const CHART_DATA = [
    {
      name: 'OVR',
      type: 'area',
      data: data != null && data.players.map((x) => x.ovr)
    },
    {
      name: 'Potential',
      type: 'line',
      data: data != null && data.players.map((x) => x.potential)
    }
  ];

  const chartOptions = merge(BaseOptionChart(), {
    stroke: { width: [2, 3] },
    plotOptions: { bar: { columnWidth: '11%', borderRadius: 4 } },
    fill: { type: ['gradient', 'solid'] },
    xaxis: { type: 'text' },
    tooltip: {
      shared: true,
      intersect: false,
      y: {
        formatter: (y) => {
          if (typeof y !== 'undefined') {
            return `${y.toFixed(0)}`;
          }
          return y;
        }
      }
    }
  });

  return (
    <Card>
      <CardHeader title={data != null && data.name} subheader={data != null && data.headerDescription} />
      <Box sx={{ p: 3, pb: 1 }} dir="ltr">
        {data === null && (
          <ReactApexChart
            type="line"
            series={CHART_DATA_MOCK}
            options={chartOptions}
            height={364}
          />
        )}
        {data !== null && (
          <ReactApexChart type="line" series={CHART_DATA} options={chartOptions} height={364} />
        )}
      </Box>
    </Card>
  );
}
