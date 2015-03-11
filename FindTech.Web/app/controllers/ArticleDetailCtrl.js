angular.module('FindTech.ArticleDetail', [])
    .controller('ArticleDetailCtrl', ['$scope', '$http', function ($scope, $http) {
        $scope.comments = {};
        $http.get('/Comment/GetComments').success(function(data) {
            $scope.comments = data;
        });
    }]);