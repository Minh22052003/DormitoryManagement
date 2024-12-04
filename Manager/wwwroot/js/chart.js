(function ($) {
    "use strict";

    let roomOccupancyLabels = [];
    let newStudentsData = [];
    let currentStudentsData = [];
    let totalStudentsData = [];
    const dataReports = {
        newStudents: 0,
        totalResidents: 0,
        studentRequests: 0,
        processedRequests: 0,
        totalRevenue: 0,
        pendingPayments: 0
    };


    let reportLabels = ["Yêu cầu đã xử lí", "Yêu cầu chưa xử lí"];
    let requestProcessedData = [];
    let requestUnprocessedData = [];

    let revenueLabels = [];
    let revenueData = [];

    let selectedDateStart, selectedDateEnd;
    let selectedDateYearStart, selectedDateYearEnd;
    let selectedMonthStart, selectedMonthEnd;
    let selectedDayStart, selectedDayEnd;

    // Debounce function to limit the rate of API calls
    function debounce(func, wait) {
        let timeout;
        return function () {
            const context = this, args = arguments;
            clearTimeout(timeout);
            timeout = setTimeout(() => func.apply(context, args), wait);
        };
    }


    // Preview and print the report
    function setupPrintButton() {
        document.getElementById('preview-button').addEventListener('click', function () {
            /*document.body.innerHTML = document.getElementById("report").innerHTML;*/
            window.print();
            window.location.reload();
        });
    }
    function setupDownloadButton() {
        document.getElementById('download-button').addEventListener('click', function () {
            /*document.body.innerHTML = document.getElementById("report").innerHTML;*/
            window.print();
            window.location.reload();
        });
    }

    function initializeCharts() {
        // Initialize the DateTimePicker for the start and end date
        $('#start-date').datetimepicker({ format: 'L' });
        $('#end-date').datetimepicker({ format: 'L' });

        $('#start-date').on('change.datetimepicker', function () {
            selectedDateStart = $('#start-date').val();
            if (selectedDateStart) {
                const startDate = new Date(selectedDateStart);
                selectedDateYearStart = startDate.getFullYear();
                selectedMonthStart = startDate.getMonth() + 1;
                selectedDayStart = startDate.getDate();
            }
            validateDates();
            updateChartData();
        });

        $('#end-date').on('change.datetimepicker', function () {
            selectedDateEnd = $('#end-date').val();
            if (selectedDateEnd) {
                const endDate = new Date(selectedDateEnd);
                selectedDateYearEnd = endDate.getFullYear();
                selectedMonthEnd = endDate.getMonth() + 1;
                selectedDayEnd = endDate.getDate();
            }
            validateDates();
            updateChartData();
        });

        function validateDates() {
            if (selectedDateStart && selectedDateEnd) {
                if (new Date(selectedDateEnd) < new Date(selectedDateStart)) {
                    alert("End date must be greater than or equal to start date. Please select a valid date range.");
                    $('#end-date').val('');
                    selectedDateEnd = null;
                    selectedDateYearEnd = null;
                    selectedMonthEnd = null;
                    selectedDayEnd = null;
                }
                if (new Date(selectedDateEnd) > new Date()) {
                    alert("End date cannot be greater than today's date. Please select a valid date.");
                    $('#end-date').val('');
                    selectedDateEnd = null;
                    selectedDateYearEnd = null;
                    selectedMonthEnd = null;
                    selectedDayEnd = null;
                }
            }
        }
    }

    // Populate data into HTML elements
    function populateData() {
        document.getElementById('total-residents').innerText = dataReports.totalResidents;
        document.getElementById('new-students').innerText = dataReports.newStudents;
        document.getElementById('student-requests').innerText = dataReports.studentRequests;
        document.getElementById('processed-requests').innerText = dataReports.processedRequests;
        document.getElementById('total-revenue').innerText = `${dataReports.totalRevenue}đ`;
    }
    function updateChartData() {
        // Check if start and end years are defined
        if (selectedDateYearStart && selectedDateYearEnd) {
            // Reset data arrays
            roomOccupancyLabels = [];
            newStudentsData = [];
            currentStudentsData = [];
            totalStudentsData = [];
            requestProcessedData = [];
            requestUnprocessedData = [];
            revenueLabels = [];
            revenueData = [];
            debounceFetchData(selectedDateStart, selectedDateEnd);
        }
    }

    function setDefaultDate() {
        const today = new Date();
        const firstDayOfMonth = new Date(today.getFullYear(), today.getMonth(), 1);

        // Format the dates using moment.js to match the 'L' format used in datetimepicker
        const formattedFirstDay = moment(firstDayOfMonth).format('L');
        const formattedToday = moment(today).format('L');

        // Set the values for start-date and end-date inputs
        $('#start-date').val(formattedFirstDay);
        $('#end-date').val(formattedToday);

        // Update the variables for selected dates
        selectedDateStart = formattedFirstDay;
        selectedDateEnd = formattedToday;

        // Convert formatted dates to Date objects for further processing
        const startDate = new Date(selectedDateStart);
        selectedDateYearStart = startDate.getFullYear();
        selectedMonthStart = startDate.getMonth() + 1;
        selectedDayStart = startDate.getDate();

        const endDate = new Date(selectedDateEnd);
        selectedDateYearEnd = endDate.getFullYear();
        selectedMonthEnd = endDate.getMonth() + 1;
        selectedDayEnd = endDate.getDate();
        updateChartData();
    }
    function addDataReports(newStudents = 0, currentResidents = 0, studentRequests = 0, processedRequests = 0, totalRevenue = 0, pendingPayments = 0) {
        dataReports.newStudents += newStudents;
        dataReports.totalResidents += currentResidents;
        dataReports.studentRequests += studentRequests;
        dataReports.processedRequests += processedRequests;
        dataReports.totalRevenue += totalRevenue;
        dataReports.pendingPayments += pendingPayments;
    }
    // Hàm cập nhật dữ liệu theo năm
    function addYearlyData(year, yearlyProcessedRequests, yearlyTotalRequests, yearlyTotalRevenue, yearlyStudents) {
        roomOccupancyLabels.push(year);
        newStudentsData.push(yearlyStudents);
        currentStudentsData.push(yearlyStudents);
        totalStudentsData.push(yearlyStudents);
        requestProcessedData.push(yearlyProcessedRequests);
        requestUnprocessedData.push(yearlyTotalRequests - yearlyProcessedRequests);
        revenueLabels.push(year);
        revenueData.push(yearlyTotalRevenue);
        addDataReports(yearlyStudents, yearlyStudents, yearlyTotalRequests, yearlyProcessedRequests, yearlyTotalRevenue);
    }

    // Hàm cập nhật dữ liệu theo tháng
    function addMonthlyData(month, monthlyProcessedRequests, monthlyTotalRequests, monthlyTotalRevenue, monthlyStudents) {
        roomOccupancyLabels.push(`Tháng ${month}`);
        newStudentsData.push(monthlyStudents);
        currentStudentsData.push(monthlyStudents);
        totalStudentsData.push(monthlyStudents);
        requestProcessedData.push(monthlyProcessedRequests);
        requestUnprocessedData.push(monthlyTotalRequests - monthlyProcessedRequests);
        revenueLabels.push(`Tháng ${month}`);
        revenueData.push(monthlyTotalRevenue);
        addDataReports(monthlyStudents, monthlyStudents, monthlyTotalRequests, monthlyProcessedRequests, monthlyTotalRevenue);

    }

    // Hàm cập nhật dữ liệu theo ngày
    function addDailyData(day, dailyProcessedRequests, dailyTotalRequests, dailyRevenue, dailyStudents) {
        roomOccupancyLabels.push(`Ngày ${day}`);
        newStudentsData.push(dailyStudents);
        currentStudentsData.push(dailyStudents);
        totalStudentsData.push(dailyStudents);
        requestProcessedData.push(dailyProcessedRequests);
        requestUnprocessedData.push(dailyTotalRequests - dailyProcessedRequests);
        revenueLabels.push(`Ngày ${day}`);
        revenueData.push(dailyRevenue);
        addDataReports(dailyStudents, dailyStudents, dailyTotalRequests, dailyProcessedRequests, dailyRevenue);
    }

    function fetchData(startDate, endDate) {

        $.ajax({
            url: `https://localhost:7249/api/ReportsAndStatistics/reportsandstatistics?firstTime=${startDate}&lastTime=${endDate}`,
            method: 'GET',
            success: function (response) {
                console.log('Data fetched:', response);
                processData(response.dailyStatistics, startDate, endDate);

                populateData();
                updateCharts();
            },
            error: function (error) {
                console.error('Error fetching data:', error);
            }
        });
    }

    const debounceFetchData = debounce(fetchData, 300);

    function processData(dailyStatistics, startDate, endDate) {
        const start = new Date(startDate);
        const end = new Date(endDate);
        const startYear = start.getFullYear();
        const endYear = end.getFullYear();
        const startMonth = start.getMonth() + 1;
        const endMonth = end.getMonth() + 1;

        if (endYear > startYear) {
            for (let year = startYear; year <= endYear; year++) {
                const yearlyData = dailyStatistics.filter(item => new Date(item.date).getFullYear() === year);
                const yearlyProcessedRequests = yearlyData.reduce((acc, item) => acc + item.processedRequests, 0);
                const yearlyTotalRequests = yearlyData.reduce((acc, item) => acc + item.totalRequests, 0);
                const yearlyRevenue = yearlyData.reduce((acc, item) => acc + item.revenue, 0);
                const yearlyStudents = yearlyData.reduce((acc, item) => acc + item.studentRegistrations, 0);
                addYearlyData(year, yearlyProcessedRequests, yearlyTotalRequests, yearlyRevenue, yearlyStudents);
            }
        } else if (startYear === endYear && endMonth > startMonth) {
            for (let month = startMonth; month <= endMonth; month++) {
                const monthlyData = dailyStatistics.filter(item => new Date(item.date).getMonth() + 1 === month);
                const monthlyProcessedRequests = monthlyData.reduce((acc, item) => acc + item.processedRequests, 0);
                const monthlyTotalRequests = monthlyData.reduce((acc, item) => acc + item.totalRequests, 0);
                const monthlyRevenue = monthlyData.reduce((acc, item) => acc + item.revenue, 0);
                const monthlyStudents = monthlyData.reduce((acc, item) => acc + item.studentRegistrations, 0);
                addMonthlyData(month, monthlyProcessedRequests, monthlyTotalRequests, monthlyRevenue, monthlyStudents);
            }
        } else {
            for (let day = start.getDate(); day <= end.getDate(); day++) {
                const dailyData = dailyStatistics.find(item => new Date(item.date).getDate() === day);
                if (dailyData) {
                    addDailyData(day, dailyData.processedRequests, dailyData.totalRequests, dailyData.revenue, dailyData.studentRegistrations);
                }
            }
        }
    }

    function updateCharts() {
        if (myChart1) {
            myChart1.data.labels = roomOccupancyLabels;
            myChart1.data.datasets[0].data = newStudentsData;
            myChart1.data.datasets[1].data = currentStudentsData;
            myChart1.data.datasets[2].data = totalStudentsData;
            myChart1.update();
        }

        if (myChart2) {
            myChart2.data.labels = reportLabels;
            myChart2.data.datasets[0].data = [
                requestProcessedData.reduce((a, b) => a + b, 0),
                requestUnprocessedData.reduce((a, b) => a + b, 0)
            ];
            myChart2.update();
        }

        if (myChart3) {
            myChart3.data.labels = revenueLabels;
            myChart3.data.datasets[0].data = revenueData;
            myChart3.update();
        }
    }

    // Initialize the chart with empty data
    var ctx1 = $("#room-occupancy").get(0).getContext("2d");
    var myChart1 = new Chart(ctx1, {
        type: "bar",
        data: {
            labels: roomOccupancyLabels,
            datasets: [
                {
                    label: "Sinh viên mới",
                    data: newStudentsData,
                    backgroundColor: "rgba(255, 99, 132, 0.7)"
                },
                {
                    label: "Sinh viên hiện tại",
                    data: currentStudentsData,
                    backgroundColor: "rgba(54, 162, 235, 0.5)"
                },
                {
                    label: "Tổng số sinh viên",
                    data: totalStudentsData,
                    backgroundColor: "rgba(75, 192, 192, 0.3)"
                }
            ]
        },
        options: {
            responsive: true,
            plugins: {
                title: {
                    display: true,
                    text: "Tình trạng sinh viên ký túc xá"
                }
            }
        }
    });

    var ctx2 = $("#reports-chart").get(0).getContext("2d");
    var myChart2 = new Chart(ctx2, {
        type: "pie",
        data: {
            labels: reportLabels,
            datasets: [
                {
                    data: [
                        requestProcessedData.reduce((a, b) => a + b, 0),
                        requestUnprocessedData.reduce((a, b) => a + b, 0)
                    ],
                    backgroundColor: [
                        "rgba(75, 192, 192, 0.7)",
                        "rgba(255, 99, 132, 0.7)"
                    ]
                }
            ]
        },
        options: {
            responsive: true,
            plugins: {
                legend: {
                    display: true,
                    position: 'top'
                },
                title: {
                    display: true,
                    text: "Tình trạng xử lý yêu cầu"
                }
            }
        }
    });

    // Revenue Chart
    var ctx3 = $("#revenue-chart").get(0).getContext("2d");
    var myChart3 = new Chart(ctx3, {
        type: "line",
        data: {
            labels: revenueLabels,
            datasets: [{
                label: "Doanh thu",
                fill: false,
                backgroundColor: "rgba(255, 159, 64, 0.3)",
                data: revenueData
            }]
        },
        options: {
            responsive: true,
            plugins: {
                title: {
                    display: true,
                    text: "Doanh thu ký túc xá"
                }
            }
        }
    });

    // Document ready function
    $(document).ready(function () {
        setupPrintButton();
        setupDownloadButton();
        initializeCharts();
        setDefaultDate();
    });

})(jQuery);  