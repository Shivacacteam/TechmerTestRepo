var app = angular.module('techmerServices', ['ngResource']);

//Resource Services

app.service("Workspace", ["$resource", "$window", function ($resource, $window) {
    return $resource("/api/Workspaces/:id", { id: '@id' }, {
        create: {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        save: {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        query: {
            isArray: true,
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        }
    })
}]) //End Workspace Service

app.service("Grid", ["$resource", "$window", function ($resource, $window) {
    return $resource("/api/grids/:id", { id: '@id' }, {
        create: {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        save: {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        query: {
            isArray: true,
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        }
    })
}]) //End Grid Service

app.service("ColorSelections", ["$resource", "$window", function ($resource, $window) {
    return $resource("/api/colorSelections/:id", { id: '@id' }, {
        save: {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        create: {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        delete: {
            method: 'DELETE',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        query: {
            isArray: true,
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        }
    })
}]) //End Color Selections Service

app.service("FavoriteColor", ["$resource", "$window", function ($resource, $window) {
    return $resource("/api/favoriteColor/:id", { id: '@id' }, {
        save: {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        create: {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        delete: {
            method: 'DELETE',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        query: {
            isArray: true,
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        }
    })
}]) //End Favorite Color Service

app.service("ProductTemplate", ["$resource", "$window", function ($resource, $window) {
    return $resource("/api/productTemplates/:id", { id: '@id' }, {
        save: {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        create: {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        delete: {
            method: 'DELETE',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        query: {
            isArray: true,
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        }
    })
}]) //End Product Template Service

app.service("Product", ["$resource", "$window", function ($resource, $window) {
    return $resource("/api/Product/:id", { id: '@id' }, {
        save: {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        create: {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        delete: {
            method: 'DELETE',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        query: {
            isArray: true,
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        }
    })
}]) //End Product Service

app.service("Asset", ["$resource", "$window", function ($resource, $window) {
    return $resource("/api/Asset/:id", { id: '@id' }, {
        save: {
            method: 'POST',
            isArray: true,
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        }
    })
}]) //End Asset Service

app.service("SampleInspiration", ["$resource", "$window", function ($resource, $window) {
    return $resource("/api/SampleInspiration/:id", { id: '@id' }, {
        save: {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        create: {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        delete: {
            method: 'DELETE',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        query: {
            isArray: true,
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        }
    })
}]) //End SampleInspiration Service

app.service("SharingTemplate", ["$resource", "$window", function ($resource, $window) {
    return $resource("/api/Sharing/:id", { id: '@id' }, {
        save: {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        create: {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        delete: {
            method: 'DELETE',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        query: {
            isArray: true,
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        list: {
            method: 'GET',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        }
    })
}]) //End sharing template Service

app.service("PersonalTemplate", ["$resource", "$window", function ($resource, $window) {
    return $resource("/api/PersonalTemplate/:id", { id: '@id' }, {
        //save: {
        //    method: 'PUT',
        //    headers: {
        //        'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
        //    }
        //},
        //create: {
        //    method: 'POST',
        //    headers: {
        //        'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
        //    }
        //},
        //delete: {
        //    method: 'DELETE',
        //    headers: {
        //        'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
        //    }
        //},
        query: {
            isArray: true,
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        }
    })
}]) //End Personal template Service

app.service("SampleRequest", ["$resource", "$window", function ($resource, $window) {
    return $resource("/api/SampleRequest/:id", { id: '@id' }, {
        save: {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        create: {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        delete: {
            method: 'DELETE',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        query: {
            isArray: true,
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        }
    })
}]) //End sample Request Service

app.service("ShareList", ["$resource", "$window", function ($resource, $window) {
    return $resource("/api/SharingList/:id", { id: '@id' }, {
        save: {
            method: 'PUT',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        create: {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        delete: {
            method: 'DELETE',
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        },
        query: {
            isArray: true,
            headers: {
                'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
            }
        }
    })
}]) //End sample Request Service

app.service("TechmerAnalytics", ["$window", function ($window) {

    var _sendEvent = function (info) {
        if (!$window.ga)
            return;

        $window.ga('send', {
            hitType: 'event',
            eventCategory: info.Category,
            eventAction: info.Action,
            eventLabel: info.Label,
            eventValue: info.eventValue
        });
    }

    this.SendEvent = function (eventInfo) {

        return _sendEvent(eventInfo);
    }

}]) //End Techmer.Analytics Service


//End of Resource Services

app.service("Color", ["ColorSelections", function (ColorSelections) {

    this.buildColor = function (colorData, lab, hsv, cmyk, Hex, workspaceId) {
       
        var lab = '';
        var hsv = '';
        var cmyk = '';
        var Hex = '';
        var ret = {};
        rgbToOthers(colorData[0], colorData[1], colorData[2]);
        ret.InternalColorString = colorData.join(",");
        if (lab != '') {
            ret.InternalLAB = lab.join(",");
            ret.LAB = lab;
        }
        if (hsv != '') {
            ret.InternalHSV = hsv.join(",");
            ret.HSV = hsv;
        }
        if (cmyk != '') {
            ret.InternalCMYK = cmyk.join(",");
            ret.CMYK = cmyk;
        }
        ret.InternalHEX = Hex;
        ret.colorData = colorData;
        ret.internalHSL = rgbToHsl(colorData[0], colorData[1], colorData[2]).join(",");
        ret.hsl = ret.internalHSL.split(",");
        ret.colorStyle = _displayColorStyle(colorData);
        ret.colorString = _displayColorData(colorData);
        ret.timeStamp = new Date().getTime();
        ret.workspaceId = workspaceId;
        ret.r = {};
        ret.r.value = colorData[0];
        ret.r.percent = Math.round(colorData[0] / 256 * 100);
        ret.g = {};
        ret.g.value = colorData[1];
        ret.g.percent = Math.round(colorData[1] / 256 * 100);
        ret.b = {};
        ret.b.value = colorData[2];
        ret.b.percent = Math.round(colorData[2] / 256 * 100);
        return ret;

        function rgbToOthers(r, g, b) {
          
            Hex = chroma(r, g, b).hex();

            lab = chroma(r, g, b).lab();

            var HSV = chroma(r, g, b).hsv();
            if (isNaN(HSV[0])) {
                HSV[0] = 0;
            }
            if (isNaN(HSV[1])) {
                HSV[1] = 0;
            }
            if (isNaN(HSV[2])) {
                HSV[2] = 0;
            }
            hsv = HSV;
            //hsv = chroma(r, g, b).hsv();
            cmyk = chroma(r, g, b).cmyk();
        }

    }

    function _displayColorStyle(color) {
        var ret = 'rgba(' + color[0].toString() + ',' + color[1].toString() + ',' + color[2].toString() + ',' + color[3].toString() + ')';
        return ret;
    }

    function _displayColorData(color) {
        var ret = "Red: " + color[0].toString() + "\n\rGreen: " + color[1].toString() + "\n\rBlue: " + color[2].toString();
        return ret;
    }
    function rgbToHsl(r, g, b) {
        r /= 255, g /= 255, b /= 255;
        var max = Math.max(r, g, b),
            min = Math.min(r, g, b);
        var h, s, l = (max + min) / 2;

        if (max == min) {
            h = s = 0; // achromatic
        } else {
            var d = max - min;
            s = l > 0.5 ? d / (2 - max - min) : d / (max + min);
            switch (max) {
                case r:
                    h = (g - b) / d + (g < b ? 6 : 0);
                    break;
                case g:
                    h = (b - r) / d + 2;
                    break;
                case b:
                    h = (r - g) / d + 4;
                    break;
            }
            h /= 6;
        }

        return ([h, s, l]);
    }
}]); //End Color Service

app.service("ColorCell", ["Color", function (Color) {
    this.buildCell = function (colorData, paintable) {
        var colorCell = Color.buildColor(colorData);
        colorCell.paintable = paintable;
        return colorCell;
    };
}])//End ColorCell Service

app.service("AssetStyleBuilder", [function () {
    this.buildAssetStyle = function (asset) {
        return {
            zIndex: asset.zIndex,
            position: 'relative',
            transform: 'translate(' + asset.x + 'px, ' + asset.y + 'px) rotate(' + asset.r + 'deg) scale(' + asset.s + ')',
            webkitTransform: 'translate(' + asset.x + 'px, ' + asset.y + 'px) rotate(' + asset.r + 'deg) scale(' + asset.s + ')'
        };
    }
}])//End AssetStyleBuilder Service

app.service("Workspaces", ["$q", "Workspace", "AssetStyleBuilder", "TechmerAnalytics", function ($q, Workspace, AssetStyleBuilder, TechmerAnalytics) {
    var Analytics = {
        Category: 'Workspace'
    }
    Analytics.ClearWorkspace = {
        Category: Analytics.Category,
        Action: 'ClearWorkspace',
        Label: 'Id',
        Value: ''
    }

    var tempWorkspace = {
        image: "",
        pixelation: 1,
    };
    var _workspaces = null;
    var deferred = null;


    function _loadData() {

        if (_workspaces !== null) {
            deferred.resolve(_workspaces);
        }
        else {
            if (deferred == null) {
                deferred = $q.defer();
                Workspace.query().$promise.then(function (data) {
                    for (i = 0; i < data.length; i++) {
                        data[i].style = AssetStyleBuilder.buildAssetStyle(data[i]);
                    }
                    _workspaces = data;
                    deferred.resolve(data);
                })
            }
        }
        return deferred.promise;
    }

    function _clearWorkspace(workspace) {
        workspace.pixelation = 1;
        workspace.image = "";
        workspace.s = 1;
        workspace.r = 0;
        workspace.x = 0;
        workspace.y = 0;
        workspace.zIndex = 0;
        workspace.comareVisible = false;
        workspace.modifiedTimeStamp = new Date().getTime();
        workspace.$save();
        Analytics.ClearWorkspace.Value = workspace.id;
        TechmerAnalytics.SendEvent(Analytics.ClearWorkspace);
        return workspace;

    }

    this.currentWorkspace = function () {
        var currentWorkspaceDeferred = $q.defer();
        _loadData().then(function (results) {
            currentWorkspaceDeferred.resolve(results[0]);
        })
        return currentWorkspaceDeferred.promise;
    }
    this.list = function () {


        return _loadData();
    };
    this.clearWorkspace = function (workspace) {
        return _clearWorkspace(workspace);
    }

    _loadData();
}]); //End Workspaces Service

app.service("Grids", ["$q", "Grid", "ColorCell", "AssetStyleBuilder", "TechmerAnalytics", function ($q, Grid, ColorCell, AssetStyleBuilder, TechmerAnalytics) {
    var _grids = null;
    var _workspaceId;
    var deferred = null;

    var Analytics = {
        Category: 'Grids'
    }
    Analytics.NewGrid = {
        Category: Analytics.Category,
        Action: 'New',
        Label: 'Id',
        Value: ''
    }
    Analytics.CopyGrid = {
        Category: Analytics.Category,
        Action: 'Copy',
        Label: 'Id',
        Value: ''
    }
    Analytics.DeleteGrid = {
        Category: Analytics.Category,
        Action: 'Delete',
        Label: 'Id',
        Value: ''
    }
    Analytics.DownloadGrid = {
        Category: Analytics.Category,
        Action: 'Download',
        Label: 'Id',
        Value: ''
    }
    Analytics.ChangeShape = {
        Category: Analytics.Category,
        Action: 'Shape',
        Label: 'Id',
        Value: ''
    }


    var _loadData = function () {
        if (_grids !== null) {
            deferred.resolve(_grids);
        } else {
            if (deferred == null) {
                deferred = $q.defer(); //

                Grid.query().$promise.then(function (data) {
                    for (i = 0; i < data.length; i++) {
                        data[i].gridArray = _initGridArray(data[i].width, data[i].height);
                        _populateCorners(data[i], data[i].topLeftColorData, data[i].topRightColorData, data[i].bottomLeftColorData, data[i].bottomRightColorData);
                        _reCalcGridValues(data[i]);
                        data[i].style = AssetStyleBuilder.buildAssetStyle(data[i]);
                        data[i].horizontalWeightView = data[i].horizontalWeight / .2;
                        data[i].verticalWeightView = data[i].verticalWeight / .2;
                    }
                    _grids = data;
                    deferred.resolve(data);

                })
            }
        }
        return deferred.promise;
    }

    var _loadRow = function (grid, array) {
        for (var i = 1; i < grid.height - 1; i++) {
            var hw = grid.horizontalWeight;
            var stepValue = ((i * (hw)) / (grid.width - 1))
            var ret = _lerp(array[0].colorData, array[(grid.width - 1)].colorData, stepValue); //v1
            if (ret.length == 3) {
                ret.push(1);//alpha
            }

            array[i] = ColorCell.buildCell(ret, false);
        }
    }

    var _loadCol = function (grid, col) {
        for (var i = 1; i < grid.width - 1; i++) {
            var stepValue = ((i * (grid.verticalWeight)) / (grid.height - 1))
            var ret = _lerp(grid.gridArray[0][col].colorData, grid.gridArray[grid.height - 1][col].colorData, stepValue);
            if (ret.length == 3) {
                ret.push(1); //alpha
            }
            grid.gridArray[i][col] = ColorCell.buildCell(ret, false);
        }
    }

    var _lerp = function (a, b, fac) {
        return a.map(function (v, i) {
            var ret = v * (1 - fac) + b[i] * fac;
            ret = Math.round(Math.max(Math.min(ret, 255), 0));
            return ret;
        });
    }

    var _reCalcGridValues = function (grid) {
        _loadCol(grid, 0);
        _loadCol(grid, grid.width - 1);

        for (var i = 0; i < grid.height; i++) {
            _loadRow(grid, grid.gridArray[i]);
        }


    }

    var _initGridArray = function (width, height) {
        var ret = [];
        for (var i = 0; i < height; i++) {
            var tempRow = [];
            for (var j = 0; j < width; j++) {
                tempRow.push(ColorCell.buildCell([255, 255, 255, 1], false));
            }
            if (i == 0)
                ret = [tempRow];
            else
                ret.push(tempRow);
        }
        return ret;
    }

    var _populateCorners = function (grid, TopLeft, TopRight, BottomLeft, BottomRight) {
        grid.gridArray[0][0] = ColorCell.buildCell(TopLeft, true);
        grid.gridArray[0][grid.width - 1] = ColorCell.buildCell(TopRight, true);
        grid.gridArray[grid.height - 1][0] = ColorCell.buildCell(BottomLeft, true);
        grid.gridArray[grid.height - 1][grid.width - 1] = ColorCell.buildCell(BottomRight, true);
    }

    var _addGrid = function (workspaceId) {
        var newGridDeferred = $q.defer();
        var newGrid = new Grid();
        newGrid.workspaceId = workspaceId;
        newGrid.height = 10;
        newGrid.width = 10;
        newGrid.internalTopLeftColorString = "255,255,255,1";
        newGrid.internalTopRightColorString = "255,255,255,1";
        newGrid.internalBottomLeftColorString = "255,255,255,1";
        newGrid.internalBottomRightColorString = "255,255,255,1";
        newGrid.topLeftColorData = [255, 255, 255, 1];
        newGrid.topRightColorData = [255, 255, 255, 1];
        newGrid.bottomLeftColorData = [255, 255, 255, 1];
        newGrid.bottomRightColorData = [255, 255, 255, 1];
        newGrid.horizontalWeight = 1;
        newGrid.verticalWeight = 1;
        newGrid.horizontalWeightView = 5;
        newGrid.verticalWeightView = 5;
        newGrid.s = 1;
        newGrid.image = "";
        newGrid.fileName = "";
        newGrid.modifiedTimeStamp = new Date().getTime();
        newGrid.gridArray = _initGridArray(newGrid.height, newGrid.width);
        _populateCorners(newGrid, newGrid.topLeftColorData, newGrid.topRightColorData, newGrid.bottomLeftColorData, newGrid.bottomRightColorData);
        _reCalcGridValues(newGrid);
        if (_grids == null) {
            _grids = [newGrid];
        } else {
            _grids.unshift(newGrid); //add to Array immediately, replace data from server on return;
        }
        newGrid.$create(
            function (data) {
                data.gridArray = _initGridArray(data.height, data.width);
                _populateCorners(data, data.topLeftColorData, data.topRightColorData, data.bottomLeftColorData, data.bottomRightColorData);
                _reCalcGridValues(data);
                _grids[_grids.indexOf(newGrid)] = data;
                data.horizontalWeightView = data.horizontalWeight / .2;
                data.verticalWeightView = data.verticalWeight / .2;
                newGridDeferred.resolve(data);
                Analytics.NewGrid.Value = data.id;
                TechmerAnalytics.SendEvent(Analytics.NewGrid);
            }
        );
        return newGridDeferred.promise;
    };

    var _copyGrid = function (GridToCopy) {

        var copyGridDeferred = $q.defer();
        var newGrid = new Grid();
        newGrid.workspaceId = GridToCopy.workspaceId;
        newGrid.internalTopLeftColorString = GridToCopy.internalTopLeftColorString;
        newGrid.internalTopRightColorString = GridToCopy.internalTopRightColorString;
        newGrid.internalBottomLeftColorString = GridToCopy.internalBottomLeftColorString;
        newGrid.internalBottomRightColorString = GridToCopy.internalBottomRightColorString;
        newGrid.topLeftColorData = GridToCopy.topLeftColorData;
        newGrid.topRightColorData = GridToCopy.topRightColorData;
        newGrid.bottomLeftColorData = GridToCopy.bottomLeftColorData;
        newGrid.bottomRightColorData = GridToCopy.bottomRightColorData;
        newGrid.horizontalWeight = GridToCopy.horizontalWeight;
        newGrid.verticalWeight = GridToCopy.verticalWeight;
        newGrid.height = GridToCopy.height;
        newGrid.width = GridToCopy.width;
        newGrid.image = GridToCopy.image;
        newGrid.spacing = GridToCopy.spacing;
        newGrid.fileName = "";
        newGrid.modifiedTimeStamp = new Date().getTime();
        _grids.unshift(newGrid);
        newGrid.$create(
            function (data) {
                _grids[_grids.indexOf(newGrid)] = data;
                copyGridDeferred.resolve(data);
                Analytics.CopyGrid.Value = data.id;
                TechmerAnalytics.SendEvent(Analytics.CopyGrid);
            }
        );
        return copyGridDeferred.promise;
    };

    var _selectGrid = function (grid) {
        grid.modifiedTimeStamp = new Date().getTime();
        _grids.sort(_sortByTimeStamp); //TODO: Split and unshift (Performance?)
        grid.$save();

    }
    var _sortByTimeStamp = function (a, b) {
        if (a.modifiedTimeStamp > b.modifiedTimeStamp) {
            return -1;
        }
        if (a.modifiedTimeStamp < b.modifiedTimeStamp) {
            return 1;
        }
        return 0;
    };

    var _deleteGrid = function (grid) {
        //Remove Product from cache
        _grids.splice(_grids.indexOf(grid), 1);

        Analytics.DeleteGrid.Value = grid.id;
        TechmerAnalytics.SendEvent(Analytics.DeleteGrid);

        //Delete from Server
        grid.$delete();
    };

    var _clearGrids = function (workspace) {
        for (i = 0; i < _grids.length; i++) {
            _deleteGrid(_grids[i]);
        }
        return _addGrid(workspace.id);
    }

    var _downloadGrid = function (event) {
        var dpiMultipler = 4;
        var div = document.getElementById('gradientGrid');
        var newDiv = div.cloneNode(true);
        newDiv.style.transform = newDiv.style.transform.concat("scale(", dpiMultipler, ",", dpiMultipler, ")");

        var renderWidth = div.clientWidth * dpiMultipler;
        var renderHeight = div.clientHeight * dpiMultipler;


        newDiv.classList.add("clearfix");
        newDiv.style.top = "5000px";
        newDiv.style.left = "550px";
        newDiv.style.position = "absolute";
        document.body.appendChild(newDiv);


        html2canvas(newDiv, {
            width: renderWidth,
            height: renderHeight,
            useCORS: true,
            onrendered: function (canvas) {
                document.body.removeChild(newDiv);
                //document.body.appendChild(canvas);

                var logo = new Image();
                logo.src = "/images/TechmerVisionLogo-md.png";
                logo.onload = function () {
                    var gridImage = new Image();
                    gridImage.src = canvas.toDataURL('image/png');
                    gridImage.onload = function () {
                        var whiteFrameSize = 5;
                        var blackFrameSize = 3;
                        var padding = 2;
                        var totalFrame = (whiteFrameSize + blackFrameSize + padding);
                        var logoSize = 100;

                        canvas.width = (canvas.width + (totalFrame * 2));
                        canvas.height = (canvas.height + logoSize + (totalFrame * 2));

                        var localctx = canvas.getContext('2d');

                        //White Background
                        localctx.fillStyle = "white";
                        localctx.rect(0, 0, canvas.width, canvas.height);
                        localctx.fill();

                        //Black Frame
                        localctx.beginPath();
                        localctx.strokeStyle = "black";
                        localctx.lineWidth = blackFrameSize;
                        localctx.rect(whiteFrameSize, whiteFrameSize, canvas.width - totalFrame, canvas.height - totalFrame);
                        localctx.stroke();

                        //Paint Grid Image
                        localctx.drawImage(gridImage, totalFrame, totalFrame);

                        //Paint Logo
                        localctx.drawImage(logo, canvas.width - 150, canvas.height - totalFrame - 71, 150, 71);

                        //Paint Contact Info
                        var textSize = 25;
                        var textPadding = 4;
                        localctx.font = "bold " + textSize + "px Arial";
                        localctx.fillStyle = "black";
                        localctx.fillText("Techmer PM", totalFrame + textPadding, canvas.height - logoSize - totalFrame + (textSize * 1) + textPadding);
                        textSize = 15;
                        localctx.font = textSize + "px Arial";
                        localctx.fillText("www.techmerpm.com", totalFrame + textPadding, canvas.height - logoSize - totalFrame + (textSize * 3));
                        localctx.fillText("web@techmerpm.com", totalFrame + textPadding, canvas.height - logoSize - totalFrame + (textSize * 4));



                        var link = document.createElement('a');
                        link.href = canvas.toDataURL();
                        link.download = '10x10Grid.png';
                        link.className + " ng-hide";
                        document.body.appendChild(link);
                        link.click();

                        Analytics.DownloadGrid.Value = _grids[0].id;
                        TechmerAnalytics.SendEvent(Analytics.DownloadGrid);
                    }
                }

            }

        })
    }

    var _setBorderRadius = function (grid, borderRadius) {
        var deferred = $q.defer();

        grid.borderRadius = borderRadius;
        deferred.resolve(grid);

        Analytics.ChangeShape.Value = grid.id;
        TechmerAnalytics.SendEvent(Analytics.ChangeShape);

        return deferred.promise;
    }

    this.list = function () {
        return _loadData();
    }
    this.reCalcGridValues = function (grid) {
        _reCalcGridValues(grid);
    }
    this.currentGrid = function () {
        var currentGridDeferred = $q.defer();
        _loadData().then(function (results) {
            currentGridDeferred.resolve(results[0]);
        })
        return currentGridDeferred.promise;
    }
    this.initGridArray = function (width, height) {
        return _initGridArray(width, height);
    }
    this.newGrid = function (workspaceId) {
        return _addGrid(workspaceId);
    }
    this.selectGrid = function (grid) {
        return _selectGrid(grid);
    }
    this.copyGrid = function (GridToCopy) {
        return _copyGrid(GridToCopy);
    }
    this.deleteGrid = function (GridToDelete) {
        return _deleteGrid(GridToDelete);
    }
    this.clearGrids = function (workspace) {
        return _clearGrids(workspace);
    }
    this.downloadGrid = function (event) {
        return _downloadGrid(event);
    }
    this.setBorderRadius = function (grid, borderRadius) {
        return _setBorderRadius(grid, borderRadius);
    }
    _loadData();
}]) //End Grids Service 

app.service("ColorPalette", ["$q", "Color", "ColorSelections", function ($q, Color, ColorSelections) {
    var _colorPalette = null;
    var _numOfRecentColor = 10;
    var deferred = null;

    var _loadData = function () {

        if (_colorPalette !== null) {
            deferred.resolve(_colorPalette);
        } else {
            if (deferred == null) {
                deferred = $q.defer();
                ColorSelections.query().$promise.then(function (data) {

                    _colorPalette = data;
                    deferred.resolve(_colorPalette);
                })
            }
        }
        return deferred.promise;
    }

    var _addColor = function (colorData, LAB, HSV, CMYK, Hex, workspaceId) {
        
        var newColorDeferred = $q.defer();
        var newColor = Color.buildColor(colorData, LAB, HSV, CMYK, Hex, workspaceId);
        var numSelectionsToDelete = _colorPalette.length - _numOfRecentColor;
        while (numSelectionsToDelete >= 0) {
            _colorPalette[_colorPalette.length - 1].$delete();
            _colorPalette.pop();
            numSelectionsToDelete--;
        }

        _colorPalette.unshift(newColor);
        ColorSelections.create(newColor).$promise.then(function (data) {
            _colorPalette[_colorPalette.indexOf(newColor)] = data;
            newColorDeferred.resolve(data);
        });
        return newColorDeferred.promise;
    };

    var _clearColorSelections = function (workspace) {
        var clearColorSelectionsDeferred = $q.defer();
        var numSelectionsToDelete = _colorPalette.length;
        while (numSelectionsToDelete > 0) {
            _colorPalette[_colorPalette.length - 1].$delete();
            _colorPalette.pop();
            numSelectionsToDelete--;
        }
        var newColor = Color.buildColor([255, 255, 255, 1], [255, 255, 255], workspace.id);
        _colorPalette.unshift(newColor);
        ColorSelections.create(newColor).$promise.then(function (data) {
            _colorPalette[_colorPalette.indexOf(newColor)] = data;
            clearColorSelectionsDeferred.resolve(data);
        });
        return clearColorSelectionsDeferred.promise;
    }

    this.addColor = function (colorData, workspaceId, LAB, HSV, CMYK, Hex) {
       
        return _addColor(colorData, LAB, HSV, CMYK, Hex, workspaceId);
    }
    this.currentColor = function () {
        var currentColorDeferred = $q.defer();
        _loadData().then(function (result) {
            currentColorDeferred.resolve(result[0]);
        })
        return currentColorDeferred.promise;
    };
    this.list = function () {
        return _loadData();
    };

    this.clearColorSelections = function (workspace) {
        return _clearColorSelections(workspace);
    }

    _loadData();

}]); //End Color Palette Service

app.service("FavoriteColorPalette", ["$q", "Color", "FavoriteColor", "TechmerAnalytics", function ($q, Color, FavoriteColor, TechmerAnalytics) {
    var _favColorPalette = null;
    var _numOfRecentColor = 9;
    var deferred = null;

    var Analytics = {
        Category: 'FavColor'
    }
    Analytics.newColor = {
        Category: Analytics.Category,
        Action: 'New',
        Label: 'RGB',
        Value: ''
    }

    function _loadData() {
        if (_favColorPalette !== null) {
            deferred.resolve(_favColorPalette);
        } else {
            if (deferred == null) {
                deferred = $q.defer();
                FavoriteColor.query(function (data) {
                    _favColorPalette = data;
                    deferred.resolve(_favColorPalette);
                })
            }
        }
        return deferred.promise;
    }

    var _setColor = function (colorData, favColor) {

        var newColor = Color.buildColor(colorData, favColor.workspaceId);
        favColor.InternalColorString = newColor.InternalColorString;
        favColor.colorData = newColor.colorData;
        favColor.internalHSL = newColor.internalHSL;
        favColor.hsl = newColor.hsl;
        favColor.colorStyle = newColor.colorStyle;
        favColor.colorString = newColor.colorString;
        favColor.$save();
        Analytics.newColor.Value = newColor.InternalColorString;
        TechmerAnalytics.SendEvent(Analytics.newColor);
        return favColor;
    };

    var _clearFavoriteColors = function (workspace) {
        for (i = 0; i < _favColorPalette.length; i++) {
            _setColor([255, 255, 255, 1], _favColorPalette[i]);
        }
        return _favColorPalette
    }

    this.setColor = function (colorData, favColor) {

        return _setColor(colorData, favColor);
    }
    this.list = function () {

        return _loadData();
    };

    this.clearFavoriteColors = function (workspace) {
        return _clearFavoriteColors(workspace);
    }

}]); //End Favorite Color Palette Service

app.service("ProductPalette", ["$rootScope", "$q", "Color", "Product", "AssetStyleBuilder", "TechmerAnalytics", "ProductTemplate", function ($rootScope, $q, Color, Product, AssetStyleBuilder, TechmerAnalytics, ProductTemplate) {
    var _productPalette = null;
    //var _numOfProducts = 9; 
    var deferred = null;

    var Analytics = {
        Category: 'ProdLib'
    }
    Analytics.newProduct = {
        Category: Analytics.Category,
        Action: 'New',
        Label: 'Id',
        Value: ''
    }
    Analytics.deleteProduct = {
        Category: Analytics.Category,
        Action: 'Delete',
        Label: 'Id',
        Value: ''
    }
    Analytics.recolorProduct = {
        Category: Analytics.Category,
        Action: 'Recolor',
        Label: 'Id',
        Value: ''
    }
    Analytics.Selected = {
        Category: Analytics.Category,
        Action: 'Upload',
        Label: 'Id',
        Value: ''
    }

    //Read and Get file
    var _readFile = function (file) {
        deferred = $q.defer();
        var fileReader = new FileReader();
        fileReader.fileName = file.name;
        fileReader.onload = function (event) {
            TechmerAnalytics.SendEvent(Analytics.Selected);
            deferred.resolve(event.target.result);
        };
        fileReader.readAsDataURL(file);
        //fileReader.readAsText(file.name);
        return deferred.promise;
    }

    var _getImage = function (uri) {
        var deferred = $q.defer();
        $http({
            Method: "GET",
            cache: false,
            responseType: "blob",
            headers: {
                "Content-Type": "application/octet-stream; charset=utf-8"
            },
            url: uri
        }).then(function successCallback(response) {

            var imagePNGMime = "image/png";
            var blob = new Blob([response.data], { type: imagePNGMime });

            fr = new FileReader();
            fr.onload = function () {
                Analytics.Selected.Value = uri;
                TechmerAnalytics.SendEvent(Analytics.Selected);
                deferred.resolve(fr.result);
            };
            fr.readAsDataURL(blob);

        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
        return deferred.promise;
    }


    this.ReadFile = function (file) {
        return _readFile(file);
    };
    this.getImage = function (uri) {
        return _getImage(uri);
    }
    

    var _UploadFile = function (title, Image, num, owner) {
        deferred = $q.defer();
        var newPT = new ProductTemplate();
        newPT.Title = title;
        newPT.Image = Image;
        newPT.BackgroundImage = null;
        newPT.NumColors = num;
        newPT.FileName = "Water";
        newPT.Owner = owner;
        ProductTemplate.create(newPT).$promise.then(function (data) {
            deferred.resolve(data);
        });

        return deferred.promise;

    }

    this.UploadFile = function (title, Image, num,owner) {
        return _UploadFile(title, Image, num, owner);
    }

    //Product Template
    var _loadData = function () {
       
        if (_productPalette !== null) {
            deferred.resolve(_productPalette);
        }
        else {
            if (deferred == null) {
                deferred = $q.defer();
                Product.query(function (data) {
                    for (i = 0; i < data.length; i++) {
                        data[i].style = AssetStyleBuilder.buildAssetStyle(data[i]);

                    }
                    _productPalette = data;
                    deferred.resolve(_productPalette);
                })
            }
        }
        return deferred.promise;
    }

    var _loadAllData = function () {

        if (_productPalette !== null) {
            deferred.resolve(_productPalette);
        }
        else {
            if (deferred == null) {
                deferred = $q.defer();
                ProductTemplate.query(function (data) {
                    for (i = 0; i < data.length; i++) {
                        data[i].style = AssetStyleBuilder.buildAssetStyle(data[i]);

                    }
                    _productPalette = data;
                    deferred.resolve(_productPalette);
                })
            }
        }
        return deferred.promise;
    }

    var _addProduct = function (productTemplate, workspaceId) {
     
        var newProductDeferred = $q.defer();
        var newPL = new Product();
        newPL.workspaceId = workspaceId;
        newPL.productTemplateId = productTemplate.id;
        newPL.title = productTemplate.title;
        newPL.image = productTemplate.image;
        newPL.ProductTemplate = productTemplate;
        newPL.hasBackgroundImage = productTemplate.hasBackgroundImage;
        newPL.backgroundImage = productTemplate.backgroundImage;
        newPL.modifiedTimeStamp = new Date().getTime();
        newPL.s = 1;
        newPL.productColors = [];
        for (i = 0; i < productTemplate.productTemplateColors.length; i++) {
            newPL.productColors.push({
                colorNumber: productTemplate.productTemplateColors[i].colorNumber,
                InternalColorString: productTemplate.productTemplateColors[i].colorData.join(","),
                colorData: productTemplate.productTemplateColors[i].colorData,
                image: productTemplate.productTemplateColors[i].image
            });
        }
        _productPalette.unshift(newPL);
        newPL.$create().then(function (result) {
            _productPalette[_productPalette.indexOf(newPL)] = result;
            //$rootScope.$broadcast('ProductLibraryChanged');
            Analytics.newProduct.Value = result.id;
            TechmerAnalytics.SendEvent(Analytics.newProduct);
            newProductDeferred.resolve(result);
        })
        return newProductDeferred.promise;
    };

    var _deleteProduct = function (product) {

        //Remove Product from cache
        _productPalette.splice(_productPalette.indexOf(product), 1);

        Analytics.deleteProduct.Value = product.id;
        TechmerAnalytics.SendEvent(Analytics.deleteProduct);

        //Delete from Server
        product.$delete();
    };

    var _sortByTimeStamp = function (a, b) {
        if (a.modifiedTimeStamp > b.modifiedTimeStamp) {
            return -1;
        }
        if (a.modifiedTimeStamp < b.modifiedTimeStamp) {
            return 1;
        }
        return 0;
    };

    var _selectProduct = function (product) {
        product.modifiedTimeStamp = new Date().getTime();
        _productPalette.sort(_sortByTimeStamp); //TODO: Split and unshift (Performance?)
        product.$save();

    }

    var _clearProductPalette = function (workspace) {
        for (var i = _productPalette.length; i > 0 ; i--) {
            _deleteProduct(_productPalette[i - 1]);
        }
        return _productPalette;
    }

    this.addProduct = function (productTemplate, workspaceId) {
        return _addProduct(productTemplate, workspaceId);
    }
    this.currentProduct = function () {
        var currentProductDeferred = $q.defer();
        _loadData()
            .then(function (results) {
                currentProductDeferred.resolve(results[0])
            }
        )
        return currentProductDeferred.promise;
    };
    this.selectProduct = function (product) {
        return _selectProduct(product);
    }
    this.list = function () {
        return _loadData();
    };

    this.allProducts = function () { return _loadAllData(); };

    this.deleteProduct = function (product) {
        _deleteProduct(product);
    };
    this.clearProductPalette = function (workspace) {
        return _clearProductPalette(workspace);
    };

    _loadData();

}]); //End Product Palette Service

app.service("AssetLayerSystem", ["$q", "Workspaces", "Grids", "ProductPalette", function ($q, Workspaces, Grids, ProductPalette) {
    var _visibleAssets = null;

    function load() {
        var deferred = $q.defer();
        if (_visibleAssets !== null) {
            deferred.resolve(_visibleAssets);
        } else {
            var gridData = Grids.list();
            var productData = ProductPalette.list();
            var workspaceData = Workspaces.list();

            $q.all([gridData, productData, workspaceData]).then(function (results) {
                var data = [];
                data = loadVisibleAssetsArray(data, results[0]);
                data = loadVisibleAssetsArray(data, results[1]);
                data = loadVisibleAssetsArray(data, results[2]);
                data.sort(_sortByzIndexTimeStamp);
                _visibleAssets = data;
                deferred.resolve(_visibleAssets);
            });
        }
        return deferred.promise;
    }

    var _sortByzIndexTimeStamp = function (a, b) {
        if (a.zIndexTimeStamp < b.zIndexTimeStamp) {
            return -1;
        }
        if (a.zIndexTimeStamp > b.zIndexTimeStamp) {
            return 1;
        }
        return 0;
    };

    var _sortByzIndex = function (a, b) {
        if (a.zIndex < b.zIndex) {
            return -1;
        }
        if (a.zIndex > b.zIndex) {
            return 1;
        }
        return 0;
    };

    function loadVisibleAssetsArray(arrAssets, arrAssetsToAdd) {
        for (i = 0; i < arrAssetsToAdd.length; i++) {
            loadVisibleAsset(arrAssets, arrAssetsToAdd[i]);
        }
        return arrAssets;
    }
    function loadVisibleAsset(arrAssets, asset) {
        if (asset.compareVisible) {
            arrAssets.push(asset);
        }
        return arrAssets;
    }
    function reIndexVisibleAssets(arrAssets) {
        var zIndex = 1;
        for (i = 0; i < arrAssets.length; i++) {
            arrAssets[i].zIndex = zIndex;
            zIndex++;
        }
        return arrAssets;
    }
    function _addAsset(asset) {
        var defer = $q.defer();
        load().then(function (result) {
            var data = [];
            data = _removeAsset(result, asset);
            data.push(asset);
            reIndexVisibleAssets(data);
            _visibleAssets = data;
            defer.resolve(_visibleAssets);
        });
        return defer.promise;
    }
    function _removeAsset(arrAssets, assetToRemove) {

        var index = arrAssets.indexOf(assetToRemove);
        if (index > -1) {
            arrAssets.splice(index, 1);
        }
        return arrAssets;

    }

    this.removeAsset = function (asset) {
        var defer = $q.defer();
        var ar = load();
        ar.then(function (result) {
            result = _removeAsset(result, asset);
            result = reIndexVisibleAssets(result);
            defer.resolve(result);
        })
        return defer;
    }
    this.addAsset = function (asset) {
        return _addAsset(asset);
    }
    this.visibleAssets = function () {
        return load();
    }
}]) //End AssetLayerSystem

app.service("SampleInspirations", ["$q", "$http", "SampleInspiration", "TechmerAnalytics", function ($q, $http, SampleInspiration, TechmerAnalytics) {
    var _sampleInspirations = null;
    var deferred = null;

    var Analytics = {
        Category: 'Inspiration'
    }
    Analytics.Selected = {
        Category: Analytics.Category,
        Action: 'SampleInspiration',
        Label: 'Id',
        Value: ''
    }

    var _loadData = function () {
        if (_sampleInspirations !== null) {
            deferred.resolve(_sampleInspirations);
        }
        else {
            if (deferred == null) {
                deferred = $q.defer();
                SampleInspiration.query(function (data) {
                    _sampleInspirations = data;
                    deferred.resolve(_sampleInspirations);
                })
            }
        }
        return deferred.promise;
    }

    var _getImage = function (uri) {
        var deferred = $q.defer();
        $http({
            Method: "GET",
            cache: false,
            responseType: "blob",
            headers: {
                "Content-Type": "application/octet-stream; charset=utf-8"
            },
            url: uri
        }).then(function successCallback(response) {

            var imagePNGMime = "image/png";
            var blob = new Blob([response.data], { type: imagePNGMime });

            fr = new FileReader();
            fr.onload = function () {
                Analytics.Selected.Value = uri;
                TechmerAnalytics.SendEvent(Analytics.Selected);
                deferred.resolve(fr.result);
            };
            fr.readAsDataURL(blob);

        }, function errorCallback(response) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
        return deferred.promise;
    }


    this.listImages = function () {
        return _loadData();
    };
    this.getImage = function (uri) {
        return _getImage(uri);
    }
    _loadData();

}]); //End Sample Inspirations Service

app.service("InspirationUpload", ["$q", "TechmerAnalytics", function ($q, TechmerAnalytics) {
    var deferred = null;

    var Analytics = {
        Category: 'Inspiration'
    }
    Analytics.Selected = {
        Category: Analytics.Category,
        Action: 'Upload',
        Label: 'Id',
        Value: ''
    }

    var _readFile = function (file) {

        deferred = $q.defer();
        var fileReader = new FileReader();
        fileReader.onload = function (event) {
            TechmerAnalytics.SendEvent(Analytics.Selected);
            deferred.resolve(event.target.result);
        };
        fileReader.readAsDataURL(file);
        return deferred.promise;
    }

    var _getImage = function (uri) {
        var deferred = $q.defer();

        deferred.resolve(uri);

        return deferred.promise;
    }


    this.ReadFile = function (file) {
        return _readFile(file);
    };
    this.getImage = function (uri) {
        return _getImage(uri);
    }

}]); //End Inspiration Upload Service

app.service("SharingPalette", ["$rootScope", "$q", "$http", "Color", "Product", "AssetStyleBuilder", "TechmerAnalytics", "SharingTemplate", "$window", "ShareList", function ($rootScope, $q, $http, Color, Product, AssetStyleBuilder, TechmerAnalytics, SharingTemplate, $window, ShareList) {
    var _sharingPalette = null;
    var deferred = null;
    var Analytics = {
        Category: 'share'
    }
    Analytics.deleteSharing = {
        Category: Analytics.Category,
        Action: 'Delete',
        Label: 'Id',
        Value: ''
    }

    var _loadData = function () {
        if (_sharingPalette !== null) {
            deferred.resolve(_sharingPalette);
        }
        else {
            if (deferred == null) {
                deferred = $q.defer();
                SharingTemplate.query(function (data) {
                    for (i = 0; i < data.length; i++) {
                        data[i].style = AssetStyleBuilder.buildAssetStyle(data[i]);

                    }
                    _sharingPalette = data;
                    deferred.resolve(_sharingPalette);
                })
            }
        }
        return deferred.promise;
    }

    this.list = function () {
        return _loadData();
    };


    var _SharedList = function (id) {
        if (_sharingPalette !== null) {
            deferred.resolve(_sharingPalette);
        }
        else {
            if (deferred == null) {
                deferred = $q.defer();
               var sdata= SharingTemplate.query(id)
                ShareList.query().$promise.then(function (data) {
                    for (i = 0; i < data.length; i++) {
                        data[i].style = AssetStyleBuilder.buildAssetStyle(data[i]);

                    }
                    _sharingPalette = data;
                    deferred.resolve(_sharingPalette);
                })
            }
        }
        return deferred.promise;
    }

    this.SharedList = function (AssetId) {
      
        return _SharedList(AssetId);
    };



    var _shareTemplate = function (ShareTo, ShareType, AssetId) {

        var newShareDeferred = $q.defer();
        var ret = {};
        ret.UserId = ShareTo;
        ret.AssetType = ShareType;
        ret.AssetId = AssetId;
        SharingTemplate.create(ret).$promise.then(function (data) {
            newShareDeferred.resolve(data);
        });
        return newShareDeferred.promise;
    };

    this.shareTemplate = function (ShareTo, ShareType, AssetId) {
        return _shareTemplate(ShareTo, ShareType, AssetId);
    }

    var _deleteShared = function (shared) {

        //Remove Product from cache
        _sharingPalette.splice(_sharingPalette.indexOf(shared), 1);
        //debugger;
        Analytics.deleteSharing.Value = shared.id;
        TechmerAnalytics.SendEvent(Analytics.deleteSharing);

        //Delete from Server
        shared.$delete();

    };


    this.deleteShared = function (shared) {

        _deleteShared(shared);
    };


    _loadData();
}]); //End Inspiration Upload Service

app.service("PersonalPalette", ["$rootScope", "$q", "Color", "Product", "AssetStyleBuilder", "TechmerAnalytics", "PersonalTemplate", function ($rootScope, $q, Color, Product, AssetStyleBuilder, TechmerAnalytics, PersonalTemplate) {
    var _personalPalette = null;
    var deferred = null;

    var _loadData = function () {
        if (_personalPalette !== null) {
            deferred.resolve(_personalPalette);
        }
        else {
            if (deferred == null) {
                deferred = $q.defer();
                PersonalTemplate.query(function (data) {
                    for (i = 0; i < data.length; i++) {
                        data[i].style = AssetStyleBuilder.buildAssetStyle(data[i]);

                    }
                    _personalPalette = data;
                    deferred.resolve(_personalPalette);
                })
            }
        }
        return deferred.promise;
    }


    this.list = function () {
        return _loadData();
    };

    _loadData();
}]); //End Inspiration Upload Service

app.service("SamplePalette", ["$rootScope", "$q", "Color", "Product", "AssetStyleBuilder", "TechmerAnalytics", "PersonalTemplate", "SampleRequest", "$http", "$window", function ($rootScope, $q, Color, Product, AssetStyleBuilder, TechmerAnalytics, PersonalTemplate, SampleRequest, $http, $window) {
    var _samplePalette = null;
    var deferred = null;

    var Analytics = {
        Category: 'sample'
    }
    Analytics.deleteRequestItem = {
        Category: Analytics.Category,
        Action: 'Delete',
        Label: 'Id',
        Value: ''
    }


    var _loadData = function () {

        if (_samplePalette !== null) {
            deferred.resolve(_samplePalette);
        }
        else {
            if (deferred == null) {
                deferred = $q.defer();
                SampleRequest.query(function (data) {
                    for (i = 0; i < data.length; i++) {
                        data[i].style = AssetStyleBuilder.buildAssetStyle(data[i]);

                    }
                    _samplePalette = data;
                    deferred.resolve(_samplePalette);
                })
            }
        }
        return deferred.promise;
    }


    this.list = function () {
        return _loadData();
    };


    var _addRequest = function (UserId, name, notes, assetList) {
        var newShareDeferred = $q.defer();
        var ret = {};
        ret.Owner = UserId;
        ret.ProjectName = name;
        ret.Notes = notes;
        ret.PublicRequestAssetlist = [];

        angular.forEach(assetList, function (value) {
            ret.PublicRequestAssetlist.push({ 'AssetId': value.assetId, 'AssetTitle': value.assetTitle, 'AssetType': value.assetType, 'Assetbackground': value.assetbackground });
        });

        //$http({
        //    method: 'POST',
        //    url: '/api/SampleRequest/PostSampleRequest',
        //    data: ret,
        //    headers: {
        //        'Authorization': 'Bearer ' + $window.sessionStorage.accessToken
        //    }
        //}).then(
        //    function (res) {
        //        console.log('succes !', res.data);    
        //    },
        //    function (err) {
        //        console.log('error...', err);
        //    }
        //);

        SampleRequest.create(ret).$promise.then(function (data) {
            newShareDeferred.resolve(data);
        });

        return newShareDeferred.promise;
    };

    this.addRequest = function (UserId, name, notes, assetList) {
        return _addRequest(UserId, name, notes, assetList);
    }


    var _updateRequest = function (Id, requestData,assetList) {

        var newShareDeferred = $q.defer();
        var ret = {};
        ret.Id = requestData.id;
        ret.Owner = requestData.owner;
        ret.ProjectName = requestData.projectName;
        ret.Notes = requestData.notes;
        ret.Status = requestData.status;
        ret.SubmissionDate = requestData.submissionDate;
        ret.CreatedDate = requestData.createdDate;
        ret.ModifiedDate = requestData.modifiedDate;
        ret.PublicRequestAssetlist = [];

        angular.forEach(assetList, function (value) {
            ret.PublicRequestAssetlist.push({ 'AssetId': value.assetId, 'AssetTitle': value.assetTitle, 'AssetType': value.assetType, 'Assetbackground': value.assetbackground });
        });
      
        SampleRequest.save(ret).$promise.then(function (data) {
            newShareDeferred.resolve(data);
        });

        return newShareDeferred.promise;
    };




    this.updateRequest = function (Id, requestData, assetList) {
        return _updateRequest(Id, requestData, assetList);
    }

    var _deleteProduct = function (product) {
       
        //Remove Product from cache
        _samplePalette.splice(_samplePalette.indexOf(product), 1);
        //debugger;
        Analytics.deleteRequestItem.Value = product.id;
        TechmerAnalytics.SendEvent(Analytics.deleteRequestItem);

        ////Delete from Server
        product.$delete();

    };


    this.deleteProduct = function (requestItem) {
  
        _deleteProduct(requestItem);
    };

    _loadData();
}]); //End Inspiration Upload Service