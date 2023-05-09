const lineChart = document.getElementById("lineChart");

new Chart(lineChart, {
    type: "line",
    data: {
        labels: [
            "Jan",
            "Feb",
            "Mar",
            "Apr",
            "May",
            "Jun",
            "Jul",
            "Aug",
            "Sep",
            "Oct",
            "Nov",
            "Dec",
        ],
        datasets: [
            {
                label: "# of Votes",
                data: [12, 19, 3, 5, 2, 3, 19, 3, 5, 2, 3, 19],
            },
            {
                label: "# of Votes",
                data: [100, 45, 34, 35, 22, 3, 45, 34, 35, 22, 3, 23],
            },
            {
                label: "# of Votes",
                data: [100, 3, 45, 34, 35, 22, 3, 45, 35, 22, 3, 60],
            },
            {
                label: "# of Votes",
                data: [35, 22, 3, 45, 35, 22, 3, 34, 35, 22, 3, 15],
            },
        ],
    },
    options: {
        tension: 0.4,
        borderWidth: 2,
        scales: {
            y: {
                beginAtZero: true,
            },
        },
    },
});

const donutChart = document.getElementById("donutChart");

new Chart(donutChart, {
    type: "doughnut",
    data: {
        labels: ["Red", "Blue", "Yellow"],
        datasets: [
            {
                label: "My First Dataset",
                data: [300, 50, 100],
                backgroundColor: [
                    "rgb(255, 99, 132)",
                    "rgb(54, 162, 235)",
                    "rgb(255, 205, 86)",
                ],
                hoverOffset: 4,
            },
        ],
    },
});
