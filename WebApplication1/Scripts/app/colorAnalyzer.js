var app = angular.module("Techmer.colorAnalyzer", ["techmerServices"]);

app.directive("colorAnalyzer", ['ColorPalette', function (ColorPalette) {
    var directive = {
        scope: {},
        restrict: 'E',
        ColorPalette:ColorPalette,
        templateUrl: "/partials/colorAnalyzer.html",
        controller: controller,
        controllerAs: 'vm',
        bindToController: true

    }
    return directive;
}]);

controller.$inject = ['$rootScope', '$scope', '$element', '$attrs', 'Color', 'ColorPalette'];

function controller($rootScope, $scope, $element, $attrs, Color, ColorPalette) {
    $scope.isOpen = true;

    var vm = this;
  
    $scope.Analyze_Colors = Analyze_Colors;

    $scope.Rgb = { Red: 0, Green: 0, Blue: 0 };
    //var dd = directive;

    loadColors();

    $rootScope.colorAnalyzerScope = $scope;
    
 
    function loadColors() {
        //debugger;
        ColorPalette.list().then(function (colors) {
            $scope.Red = colors[0].colorData[0];
            $scope.Green = colors[0].colorData[1];
            $scope.Blue = colors[0].colorData[2];
        })
    }
    this.init = function () {
        vm.diff = {
            r: {
                value: 0,
                percent: 0
            },
            g: {
                value: 0,
                percent: 0
            },
            b: {
                value: 0,
                percent: 0
            },
        }
        //vm.color1 = $localStorage.Rgb;
        vm.color1 = Color.buildColor([255, 255, 255, 1], 0);
        vm.color2 = Color.buildColor([255, 255, 255, 1], 0);
        vm.calculateDiff();
    }


    interact('.colorAnalyzerDrop').dropzone({
        accept: '.colorDraggable',
        overlap: 0.50,
        ondropactivate: function (event) {
            event.target.classList.add('drop-active');
        },
        ondragenter: function (event) {
            var draggableElement = event.relatedTarget,
                dropzoneElement = event.target;
            dropzoneElement.classList.add('drop-target');
            draggableElement.classList.add('can-drop');
        },
        ondragleave: function (event) {
            event.target.classList.remove('drop-target');
            event.relatedTarget.classList.remove('can-drop');
        },
        ondrop: function (event) {
            var index = event.target.getAttribute('data-index').toString();
            var colorData = event.relatedTarget.getAttribute('data-color').toString().replace("[", "").replace("]", "").split(",");
            colorData[0] = parseInt(colorData[0]);
            colorData[1] = parseInt(colorData[1]);
            colorData[2] = parseInt(colorData[2]);
            colorData[3] = parseInt(colorData[3]);
            vm.setColor(colorData, index);

        },
        ondropdeactivate: function (event) {
            event.target.classList.remove('drop-active');
            event.target.classList.remove('drop-target');
        }
    });


    vm.setColor = function (colorData, target) {
        if (target == '1') {
            vm.color1 = Color.buildColor(colorData, 0);
            $scope.$apply();
        } else if (target == '2' ){
            vm.color2 = Color.buildColor(colorData, 0);
            $scope.$apply();
        }
        this.calculateDiff();
    };

    vm.calculateDiff = function () {
        vm.diff.r.value = Math.abs(vm.color1.r.value - vm.color2.r.value);
        vm.diff.r.percent = Math.round(vm.diff.r.value / 256 * 100);
        vm.diff.g.value = Math.abs(vm.color1.g.value - vm.color2.g.value);
        vm.diff.g.percent = Math.round(vm.diff.g.value / 256 * 100);
        vm.diff.b.value = Math.abs(vm.color1.b.value - vm.color2.b.value);
        vm.diff.b.percent = Math.round(vm.diff.b.value / 256 * 100);
    }
    $scope.$on('clearWorkspace', function (event, args) {
        this.init();
    });

    this.init();
            
}
