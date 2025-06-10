$(function () {
    /* ChartJS
* -------
* Here we will create a few charts using ChartJS
*/

    ////--------------
    ////- AREA CHART -
    ////--------------

    //// Get context with jQuery - using jQuery's .get() method.
    //var areaChartCanvas = $('#areaChart').get(0).getContext('2d')

    //var areaChartData = {
    //    labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
    //    datasets: [
    //        {
    //            label: 'Digital Goods',
    //            backgroundColor: 'rgba(60,141,188,0.9)',
    //            borderColor: 'rgba(60,141,188,0.8)',
    //            pointRadius: false,
    //            pointColor: '#3b8bba',
    //            pointStrokeColor: 'rgba(60,141,188,1)',
    //            pointHighlightFill: '#fff',
    //            pointHighlightStroke: 'rgba(60,141,188,1)',
    //            data: [28, 48, 40, 19, 86, 27, 90]
    //        },
    //        {
    //            label: 'Electronics',
    //            backgroundColor: 'rgba(210, 214, 222, 1)',
    //            borderColor: 'rgba(210, 214, 222, 1)',
    //            pointRadius: false,
    //            pointColor: 'rgba(210, 214, 222, 1)',
    //            pointStrokeColor: '#c1c7d1',
    //            pointHighlightFill: '#fff',
    //            pointHighlightStroke: 'rgba(220,220,220,1)',
    //            data: [65, 59, 80, 81, 56, 55, 40]
    //        },
    //    ]
    //}

    //var areaChartOptions = {
    //    maintainAspectRatio: false,
    //    responsive: true,
    //    legend: {
    //        display: false
    //    },
    //    scales: {
    //        xAxes: [{
    //            gridLines: {
    //                display: false,
    //            }
    //        }],
    //        yAxes: [{
    //            gridLines: {
    //                display: false,
    //            }
    //        }]
    //    }
    //}

    //// This will get the first returned node in the jQuery collection.
    //new Chart(areaChartCanvas, {
    //    type: 'line',
    //    data: areaChartData,
    //    options: areaChartOptions
    //})

    ////-------------
    ////- LINE CHART -
    ////--------------
    //var lineChartCanvas = $('#lineChart').get(0).getContext('2d')
    //var lineChartOptions = $.extend(true, {}, areaChartOptions)
    //var lineChartData = $.extend(true, {}, areaChartData)
    //lineChartData.datasets[0].fill = false;
    //lineChartData.datasets[1].fill = false;
    //lineChartOptions.datasetFill = false

    //var lineChart = new Chart(lineChartCanvas, {
    //    type: 'line',
    //    data: lineChartData,
    //    options: lineChartOptions
    //})

    ////-------------
    ////- DONUT CHART -
    ////-------------
    //// Get context with jQuery - using jQuery's .get() method.
    //var donutChartCanvas = $('#donutChart').get(0).getContext('2d')
    //var donutData = {
    //    labels: [
    //        'Chrome',
    //        'IE',
    //        'FireFox',
    //        'Safari',
    //        'Opera',
    //        'Navigator',
    //    ],
    //    datasets: [
    //        {
    //            data: [700, 500, 400, 600, 300, 100],
    //            backgroundColor: ['#f56954', '#00a65a', '#f39c12', '#00c0ef', '#3c8dbc', '#d2d6de'],
    //        }
    //    ]
    //}
    //var donutOptions = {
    //    maintainAspectRatio: false,
    //    responsive: true,
    //}
    ////Create pie or douhnut chart
    //// You can switch between pie and douhnut using the method below.
    //new Chart(donutChartCanvas, {
    //    type: 'doughnut',
    //    data: donutData,
    //    options: donutOptions
    //})

    ////-------------
    ////- PIE CHART -
    ////-------------
    //// Get context with jQuery - using jQuery's .get() method.
    //var pieChartCanvas = $('#pieChart').get(0).getContext('2d')
    //var pieData = donutData;
    //var pieOptions = {
    //    maintainAspectRatio: false,
    //    responsive: true,
    //}
    ////Create pie or douhnut chart
    //// You can switch between pie and douhnut using the method below.
    //new Chart(pieChartCanvas, {
    //    type: 'pie',
    //    data: pieData,
    //    options: pieOptions
    //})

    ////-------------
    ////- BAR CHART -
    ////-------------
    //var barChartCanvas = $('#barChart').get(0).getContext('2d')
    //var barChartData = $.extend(true, {}, areaChartData)
    //var temp0 = areaChartData.datasets[0]
    //var temp1 = areaChartData.datasets[1]
    //barChartData.datasets[0] = temp1
    //barChartData.datasets[1] = temp0

    //var barChartOptions = {
    //    responsive: true,
    //    maintainAspectRatio: false,
    //    datasetFill: false
    //}

    //new Chart(barChartCanvas, {
    //    type: 'bar',
    //    data: barChartData,
    //    options: barChartOptions
    //})

    ////---------------------
    ////- STACKED BAR CHART -
    ////---------------------
    //var stackedBarChartCanvas = $('#stackedBarChart').get(0).getContext('2d')
    //var stackedBarChartData = $.extend(true, {}, barChartData)

    //var stackedBarChartOptions = {
    //    responsive: true,
    //    maintainAspectRatio: false,
    //    scales: {
    //        xAxes: [{
    //            stacked: true,
    //        }],
    //        yAxes: [{
    //            stacked: true
    //        }]
    //    }
    //}

    //new Chart(stackedBarChartCanvas, {
    //    type: 'bar',
    //    data: stackedBarChartData,
    //    options: stackedBarChartOptions
    //})

    let donutChart, pieChart, barChart;

    //$.ajax({
    //    url: 'https://localhost:7079/api/Employees/chart/active-per-department',
    //    type: 'GET',
    //    dataType: 'json',
    //    success: function (response) {
    //        const data = response.data;

    //        const labels = data.map(item => item.departmentName);
    //        const values = data.map(item => item.totalEmployees);

    //        console.log("Labels:", labels);
    //        console.log("Values:", values);

    //        const backgroundColors = [
    //            '#f56954', '#00a65a', '#f39c12', '#00c0ef',
    //            '#3c8dbc', '#d2d6de', '#8e44ad', '#1abc9c',
    //            '#e74c3c', '#2ecc71', '#3498db', '#e67e22'
    //        ];

    //        // ---------- 1. DONUT CHART ----------
    //        const donutCtx = $('#donutChart').get(0).getContext('2d');
    //        if (donutChart) donutChart.destroy();
    //        donutChart = new Chart(donutCtx, {
    //            type: 'doughnut',
    //            data: {
    //                labels: labels,
    //                datasets: [{
    //                    data: values,
    //                    backgroundColor: backgroundColors.slice(0, labels.length),
    //                }]
    //            },
    //            options: {
    //                responsive: true,
    //                maintainAspectRatio: false,
    //                plugins: {
    //                    legend: {
    //                        display: true,
    //                        position: 'top'
    //                    }
    //                }
    //            }
    //        });

    //        // ---------- 2. PIE CHART ----------
    //        const pieCtx = $('#pieChart').get(0).getContext('2d');
    //        if (pieChart) pieChart.destroy();
    //        pieChart = new Chart(pieCtx, {
    //            type: 'pie',
    //            data: {
    //                labels: labels,
    //                datasets: [{
    //                    data: values,
    //                    backgroundColor: backgroundColors.slice(0, labels.length),
    //                }]
    //            },
    //            options: {
    //                responsive: true,
    //                maintainAspectRatio: false,
    //                plugins: {
    //                    legend: {
    //                        display: true,
    //                        position: 'top'
    //                    }
    //                }
    //            }
    //        });

    //        // ---------- 3. BAR CHART ----------
    //        const barCtx = $('#barChart').get(0).getContext('2d');
    //        if (barChart) barChart.destroy();
    //        barChart = new Chart(barCtx, {
    //            type: 'bar',
    //            data: {
    //                labels: labels,
    //                datasets: [{
    //                    label: 'Jumlah Karyawan Aktif',
    //                    data: values,
    //                    backgroundColor: '#3c8dbc'
    //                }]
    //            },
    //            options: {
    //                responsive: true,
    //                maintainAspectRatio: false,
    //                scales: {
    //                    y: {
    //                        min: 0,
    //                        //max: 4, // nilai tertinggi + 1 agar stepSize 1 bisa dipakai
    //                        ticks: {
    //                            stepSize: 1,
    //                            callback: function (value) {
    //                                return Number.isInteger(value) ? value : '';
    //                            }
    //                        },
    //                        grid: {
    //                            drawBorder: true
    //                        }
    //                    },
    //                    x: {
    //                        grid: {
    //                            drawBorder: true
    //                        }
    //                    }
    //                },
    //                plugins: {
    //                    legend: {
    //                        display: true,
    //                        position: 'top'
    //                    }
    //                }
    //            }
    //        });
    //    },
    //    error: function () {
    //        Swal.fire({
    //            icon: 'error',
    //            title: 'Gagal memuat data chart',
    //            timer: 2000
    //        });
    //    }
    //});


    $.ajax({
        url: 'https://localhost:7079/api/Employees/chart/active-per-department',
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            const data = response.data;

            const labels = data.map(item => item.departmentName);
            const values = data.map(item => item.totalEmployees);

            //// Debug output
            //console.log("Labels:", labels);
            //console.log("Values:", values);

            const backgroundColors = [
                '#f56954', '#00a65a', '#f39c12', '#00c0ef',
                '#3c8dbc', '#d2d6de', '#8e44ad', '#1abc9c',
                '#e74c3c', '#2ecc71', '#3498db', '#e67e22'
            ];

            // ---------- 1. DONUT CHART ----------
            const donutCtx = $('#donutChart').get(0).getContext('2d');
            if (donutChart) donutChart.destroy();
            donutChart = new Chart(donutCtx, {
                type: 'doughnut',
                data: {
                    labels: labels,
                    datasets: [{
                        data: values,
                        backgroundColor: backgroundColors.slice(0, labels.length),
                    }]
                },
                options: {
                    maintainAspectRatio: false,
                    responsive: true
                }
            });

            // ---------- 2. PIE CHART ----------
            const pieCtx = $('#pieChart').get(0).getContext('2d');
            if (pieChart) pieChart.destroy();
            pieChart = new Chart(pieCtx, {
                type: 'pie',
                data: {
                    labels: labels,
                    datasets: [{
                        data: values,
                        backgroundColor: backgroundColors.slice(0, labels.length),
                    }]
                },
                options: {
                    maintainAspectRatio: false,
                    responsive: true
                }
            });

            // ---------- 3. BAR CHART ----------
            const barCtx = $('#barChart').get(0).getContext('2d');
            if (barChart) barChart.destroy();
            barChart = new Chart(barCtx, {
                type: 'bar',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Jumlah Karyawan Aktif',
                        data: values,
                        backgroundColor: '#3c8dbc'
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true, // Mulai dari 0
                                min: 0,            // Pastikan tidak auto-scale dari 1
                                stepSize: 1,       // Biar naiknya 1 per garis
                                precision: 0       // Biar tidak desimal
                            },
                            gridLines: {
                                drawBorder: true
                            }
                        }],
                        xAxes: [{
                            gridLines: {
                                drawBorder: true
                            }
                        }]
                    },
                    legend: {
                        display: true
                    }
                }
            });
        },
        error: function () {
            Swal.fire({
                icon: 'error',
                title: 'Gagal memuat data chart',
                timer: 2000
            });
        }
    });
})