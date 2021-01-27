
Highcharts.chart('container', {
    title: {
        text: 'Biến động nhân sự'
    },
    xAxis: {
        categories: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12',]
    },

    series: [{
        type: 'column',
        name: 'Nhân viên mới',
        data: [3, 2, 1, 3, 4, 5, 6, 5, 7, 8, 9, 6]
    }, {
        type: 'column',
        name: 'Nhân viên nghỉ việc',
        data: [3, 2, 1, 3, 4, 5, 6, 5, 7, 8, 9, 6]
    }, {
        type: 'column',
        name: 'Nhân viên hiện có',
        data: [4, 3, 3, 9, 0, 5, 7, 4, 6, 9, 2, 6]
    }, {
        type: 'spline',
        name: 'Trung bình',
        data: [3, 2.67, 3, 6.33, 3.33, 4.4, 4.8, 4.9, 5, 6, 7, 4],
        marker: {
            lineWidth: 3,
            lineColor: Highcharts.getOptions().colors[2],
            fillColor: 'white'
        }

    }]
});