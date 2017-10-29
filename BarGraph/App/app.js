var app = angular.module("barGraph", ["ngRoute", "ngFileUpload", "chart.js"]);

app.config(function($routeProvider) {
    $routeProvider
    .when("/", {
        templateUrl: "/app/main.html",
        controller: "mainCtrl"
    });
});

app.controller('mainCtrl', ['$scope', 'Upload', '$timeout', '$http', function ($scope, Upload, $timeout, $http) {

    $scope.validFile = true;
    $scope.graphData = [];

    $scope.chartSettings = {
        labels : [],
        colors : [],
        data : [],
        autoUpdate: true,
        updateInterval : 3000
    };

    $scope.$watch('chartSettings.autoUpdate', function (newValue, oldValue) {
        if (newValue === true && oldValue === false) {
            updateGraphData();
        }
    });

    $scope.uploadFiles = function (file, errFiles) {
        $scope.file = file;
        $scope.errFile = errFiles && errFiles[0];

        if (file) {
            file.upload = Upload.upload({
                url: '/api/graph',
                data: { file: file }
            });

            file.upload.then(function (response) {
                $timeout(function () {
                    file.result = response.data;
                    
                    $scope.graphData = response.data.data;
                    $scope.validFile = response.data.validFile;

                    updateChart();

                    if ($scope.validFile) {
                        $timeout(updateGraphData, 1000);
                    }
                    
                });
            }, function (response) {
                if (response.status > 0)
                    $scope.errorMsg = response.status + ': ' + response.data;
            }, function (evt) {
                file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
            });
        }
    };

    function updateGraphData() {
        $http.get("/api/graph")
            .then(function (response) {
                if ($scope.validFile && $scope.chartSettings.autoUpdate) {
                    $scope.graphData = response.data;
                    updateChart();
                    $timeout(updateGraphData, $scope.chartSettings.updateInterval);
                }
            });
    }

    function updateChart() {
        if ($scope.validFile) {
            //map graph data to chart format
            var labels = $scope.graphData.map(function (item, index) { return item.name; });
            var colors = $scope.graphData.map(function (item, index) { return item.colorHex; });
            var values = $scope.graphData.map(function (item, index) { return item.value; });

            $scope.chartSettings.labels = labels;
            $scope.chartSettings.colors = colors;
            $scope.chartSettings.data = values;
        } else {
            $scope.chartSettings.labels = [];
            $scope.chartSettings.colors = [];
            $scope.chartSettings.data = [];
        }
    }

}]);
