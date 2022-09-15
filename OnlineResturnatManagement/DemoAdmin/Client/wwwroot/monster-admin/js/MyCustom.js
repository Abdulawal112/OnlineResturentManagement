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
    enabledAnchors: ['middle-left', 'middle-right', 'bottom-center', 'top-center'],
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
    var table = new Konva.Label({
        x: layerOfX,
        y: layerOfY,
        draggable: true,
        width: 200,
        height: 200,
        stroke: 1,
        strokeWidth:1,
    });

     layerOfX =0;
     layerOfY = 0;

    //textNode
    var textNode = new Konva.Text({
        width: 200,
        height: 200,
        x: layerOfX,
        y: layerOfY,
        text: 'Table No : ',
        verticalAlign: 'middle',
        align: 'center',
        fontSize: 20,
        fontFamily: 'Calibri',
        fill: 'black',
        listening: false,
        opacity: 1,
        fontWeight: 'bolder',
        position:'relative'
    });

    textNode.setZIndex(999);
    table.add(textNode);
    table.add(tr);
    table.add(selectionRectangle);

    var imageObj = new Image({ width: 200, height: 200 });

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
        console.log('e');
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

    //for editable text
    table.on('dblclick dbltap', (evt) => {

        var textPosition = table.absolutePosition();
        var areaPosition = {
            x: stage.container().offsetLeft + textPosition.x,
            y: stage.container().offsetTop + textPosition.y,
        };

            var textarea = document.createElement('textarea');
            document.body.appendChild(textarea);

            textarea.value = textNode.text();
            textarea.style.position = 'absolute';
            textarea.style.top = (areaPosition.y+85) + 'px';
            textarea.style.left = (areaPosition.x+70) + 'px';
            textarea.style.width = textNode.width();


            textarea.focus();

            textarea.addEventListener('keydown', function (e) {
                if (e.keyCode === 13) {
                    textNode.text(textarea.value);
                    document.body.removeChild(textarea);
                }
            });
        
    });

   /* for draging image*/
    Konva.Image.fromURL(itemURL, function (img) {

        var rect = new Konva.Rect({
            width: 200,
            height: 200,
            strokeWidth: 0.6,
            fillPatternImage: imageObj,
            fillPatternRepeat: 'no-repeat',
            /*  fillPatternOffset: { x: 400, y: 20 },*/
            /* fillPatternScaleX: 200,*/
            //fillPatternScaleY: 50,
            //fillPatternScaleX:100,
            opacity: 0.6,
            stroke: 'black',

        });
        rect.setZIndex(99);
        table.add(rect);
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

stage.addEventListener("click", () => {
    var json = stage.toJSON();
    console.log(json);
});