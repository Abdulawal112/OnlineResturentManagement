var width = window.innerWidth;
var height = window.innerHeight;


var stage = new Konva.Stage({
    container: 'container',
    width: width,
    height: height,
    position:'relative'
});

var layer = new Konva.Layer({
    draggable: true,
});

stage.add(layer);


var itemURL = '';
document
    .getElementById('drag-items')
    .addEventListener('dragstart', function (e) {
        itemURL = e.target.src;
    });

var con = stage.container();
con.addEventListener('dragover', function (e) {
    e.preventDefault(); // !important
});

//transform row
var tr = new Konva.Transformer({
    enabledAnchors: ['middle-left', 'middle-right', 'bottom-center'],
});

//selection rectangular
var selectionRectangle = new Konva.Rect({
    fill: 'rgba(0,0,255,0.5)',
    visible: false,
});


const getClassOfCanvas = document.getElementsByClassName('konvajs-content');
const getCanvasContainer = document.getElementById('container');

//Image DragDrop
con.addEventListener('drop', function (e) {

    var layerOfX = e.layerX;
    var layerOfY = e.layerY;
    //main Parent label
    var table = new Konva.Group({
        x: layerOfX,
        y: layerOfY,
        draggable: true,
        //width: 200,
        //height: 200,
        stroke: 1,
        strokeWidth: 1,
        opacity:0.95
    });

     layerOfX =0;
     layerOfY = 0;
    var button = new Konva.Label({
        width: 20,
        height:20,
        x: layerOfX,
        y: layerOfY,
        opacity: 0.75
    });

    //layer.add(button);

    button.add(new Konva.Tag({
        fill: 'black',
        lineJoin: 'round',
        shadowColor: 'black',
        shadowBlur: 10,
        shadowOffset: 10,
        shadowOpacity: 0.5
    }));
    var buttonTextNodeRemove = new Konva.Text({
        width: 60,
        height: 20,
        x: layerOfX,
        y: layerOfY,
        opacity: 1,
        text: 'Remove',
        verticalAlign: 'bottom',
        align: 'left',
        fontSize: 18,
        fontFamily: 'Calibri',
        fill: 'white',
        color: 'white',
        listening: true,
        fontWeight: '700',
        position: 'relative',
    });

    var buttonTextNodeBookTable = new Konva.Text({
        width: 200,
        height: 20,
        x: layerOfX,
        y: layerOfY,
        opacity: 1,
        text: 'Book Table',
        verticalAlign: 'bottom',
        align: 'right',
        fontSize: 18,
        fontFamily: 'Calibri',
        fill: 'white',
        color: 'white',
        listening: false,
        fontWeight: '700',
        position: 'relative',
    });
    button.add(buttonTextNodeBookTable);
    button.add(buttonTextNodeRemove);

    //textNode
    var textNode = new Konva.Text({
        width: 200,
        height: 200,
        x: layerOfX,
        y: layerOfY,
        opacity:1,
        text: 'Table No : ',
        verticalAlign: 'middle',
        align: 'center',
        fontSize: 22,
        fontFamily: 'Calibri',
        fill: 'white',
        color:'white',
        listening: false,
        fontWeight: '700',
        position: 'relative',
    });
    textNode.zIndex(99);
    table.add(tr);
    table.add(selectionRectangle);

    var imageObj = new Image();
    imageObj.onload = function () {
        width: 200;
        height: 200;
    };
    imageObj.src = itemURL;
    e.preventDefault();
    layer.add(table);

    //selection remove stage
    var x1, y1, x2, y2;
    stage.on('mousedown touchstart', (e) => {
        // do nothing if we mousedown on any shape
        if (e.target !== stage) {
            return;
        }
        e.evt.preventDefault();
        x1 = stage.getPointerPosition().x;
        y1 = stage.getPointerPosition().y;
        x2 = stage.getPointerPosition().x;
        y2 = stage.getPointerPosition().y;

        selectionRectangle.visible(true);
        selectionRectangle.width(0);
        selectionRectangle.height(0);
    });

    stage.on('mousemove touchmove', (e) => {
        // do nothing if we didn't start selection
        if (!selectionRectangle.visible()) {
            return;
        }
        e.evt.preventDefault();
        x2 = stage.getPointerPosition().x;
        y2 = stage.getPointerPosition().y;

        selectionRectangle.setAttrs({
            x: Math.min(x1, x2),
            y: Math.min(y1, y2),
            width: Math.abs(x2 - x1),
            height: Math.abs(y2 - y1),
        });
    });

    stage.on('mouseup touchend', (e) => {
        if (!selectionRectangle.visible()) {
            return;
        }
        e.evt.preventDefault();
        setTimeout(() => {
            selectionRectangle.visible(false);
        });

        var shapes = stage.find('.rect');
        var box = selectionRectangle.getClientRect();
        var selected = shapes.filter((shape) =>
            Konva.Util.haveIntersection(box, shape.getClientRect())
        );
        tr.nodes(selected);
    });

    // clicks should select/deselect shapes
    table.on('click tap', function (e) {
        /*console.log('e');*/
        if (selectionRectangle.visible()) {
            return;
        }
        if (e.target === stage) {
            tr.nodes([]);
            return;
        }
       
        const metaPressed = e.evt.shiftKey || e.evt.ctrlKey || e.evt.metaKey;
        const isSelected = tr.nodes().indexOf(e.target) >= 0;

        if (!metaPressed && !isSelected) {
            tr.nodes([e.target]);
        } else if (metaPressed && isSelected) {

            const nodes = tr.nodes().slice(); 
            nodes.splice(nodes.indexOf(e.target), 1);
            tr.nodes(nodes);
        } else if (metaPressed && !isSelected) {
            const nodes = tr.nodes().concat([e.target]);
            tr.nodes(nodes);
        }
    });

    buttonTextNodeRemove.addEventListener('click', (evt) => {
        console.log(evt);
        evt.target.remove();
    });

    //for editable text
    table.on('dblclick dbltap', (evt) => {
        const getClassEditedTable = document.getElementsByClassName('edit-table-no');
        if (getClassEditedTable.length > 0) {
            alert('Please Close Editable Form First');
        }
        else {
            var textPosition = table.absolutePosition();
            var areaPosition = {
                x: stage.container().offsetLeft + textPosition.x,
                y: stage.container().offsetTop + textPosition.y,
            };

            var textarea = document.createElement('textarea');
            textarea.classList.add("edit-table-no");
            document.body.appendChild(textarea);

            textarea.value = textNode.text();
            textarea.style.position = 'absolute';
            textarea.style.top = (areaPosition.y + 85) + 'px';
            textarea.style.left = (areaPosition.x + 80) + 'px';
            textarea.style.width = textNode.width();


            textarea.focus();

            textarea.addEventListener('keydown', function (e) {
                if (e.keyCode === 13) {
                    textNode.text(textarea.value);
                    document.body.removeChild(textarea);
                }
            });
        }
       
        
    });

   /* for draging image*/
    Konva.Image.fromURL(itemURL, function (img) {

        var rect = new Konva.Rect({
            width: 200,
            height: 200,
            strokeWidth: 0.6,
           /* fill:'yellow',*/
            fillPatternImage: imageObj,
            fillPatternRepeat: 'no-repeat',
            /*  fillPatternOffset: { x: 400, y: 20 },*/
            fillPatternScaleX: 0.5,
            fillPatternScaleY: 0.5,
            opacity:1,
            stroke: 'black',
            position:'absolute'
        });
        rect.zIndex(0);
        table.add(rect);
        table.add(textNode);
        table.add(button);
        tr.nodes([rect]);
    });

});


//select background canvas
const SelectedBg = document.getElementById('bg_image');

SelectedBg.addEventListener('change', ({ target }) => {
    if (target.files[0]) {
        const srcOfImage = URL.createObjectURL(target.files[0]);
        getCanvasContainer.style.backgroundImage = `url(${srcOfImage})`;
        getCanvasContainer.onload = function () {
            URL.revokeObjectURL(getCanvasContainer.src) // free memory
        }
    };
});

var TableManagementJson;
const getSavedFloorClicked = document.getElementById('saveFloor');
getSavedFloorClicked.addEventListener('click', () => {
    var json = stage.toJSON();
    TableManagementJson = json;
});



stage.addEventListener("click", () => {
    var json = stage.toJSON();
    console.log(json);
});

/*export default TableManagementJson;*/