var width = window.innerWidth;
var height = window.innerHeight;

var stage = new Konva.Stage({
    container: 'container',
    width: width,
    height: height,
});

var layer = new Konva.Layer({
    draggable: true,
});



//var imageObj = new Image();
//imageObj.src = 'https://cdn3.vectorstock.com/i/1000x1000/12/57/a-simple-nature-scene-vector-23891257.jpg';

//imageObj.onload = function () {
//    var map = new Konva.Image({
//        x: 0,
//        y: 0,
//        image: imageObj,
//        width: width,
//        height: height,
//    });
//    layer.add(map);
//}

stage.add(layer);


function getCrop(image, size, clipPosition = 'center-middle') {
    const width = size.width;
    const height = size.height;
    const aspectRatio = width / height;

    let newWidth;
    let newHeight;

    const imageRatio = image.width / image.height;

    if (aspectRatio >= imageRatio) {
        newWidth = image.width;
        newHeight = image.width / aspectRatio;
    }
    else
    {
        newWidth = image.height * aspectRatio;
        newHeight = image.height;
    }

    let x = 0;
    let y = 0;

    if (clipPosition === 'center-middle') {
        x = (image.width - newWidth) / 2;
        y = (image.height - newHeight) / 2;
    
    } else if (clipPosition === 'scale') {
        x = 0;
        y = 0;
        newWidth = width;
        newHeight = height;
    } else {
        console.error(
            new Error('Unknown clip position property - ' + clipPosition)
        );
    }

    return {
        cropX: x,
        cropY: y,
        cropWidth: newWidth,
        cropHeight: newHeight,
    };
}



// what is url of dragging element?
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

// function to apply crop
function applyCrop(pos) {
    const img = layer.findOne('.image');
    img.setAttr('lastCropUsed', pos);
    const crop = getCrop(
        img.image(),
        { width: img.width(), height: img.height() },
        pos
    );
    img.setAttrs(crop);
}

const getClassOfCanvas = document.getElementsByClassName('konvajs-content');

//Image DragDrop
con.addEventListener('drop', function (e) {
    //var textNode = new Konva.Text({
    //    text: 'Some text here',
    //    x: 50,
    //    y: 50,
    //    fontSize: 12,
    //    draggable: true
    //});
    e.preventDefault();
    stage.setPointersPositions(e);
    /*layer.add(textNode);*/

    //var textNode = new Konva.Text({
    //    text: 'Some text here',
    //    x: 50,
    //    y: 50,
    //    fontSize: 20,
    //});

    //layer.add(textNode);
    //var textPosition = textNode.getAbsolutePosition();

    //// then lets find position of stage container on the page:
    //var stageBox = stage.container().getBoundingClientRect();

    //// so position of textarea will be the sum of positions above:
    //var areaPosition = {
    //    x: stageBox.left + textPosition.x,
    //    y: stageBox.top + textPosition.y,
    //};

    //// create textarea and style it
    //var textarea = document.createElement('textarea');
    //document.body.appendChild(textarea);

    //textarea.value = textNode.text();
    //textarea.style.position = 'absolute';
    //textarea.style.top = areaPosition.y + 'px';
    //textarea.style.left = areaPosition.x + 'px';
    //textarea.style.width = textNode.width();

    Konva.Image.fromURL(itemURL, function (image) {

        image.setAttrs({
            width: 200,
            height: 200,
            name: 'image',
            draggable: true,
            fill: 'black',
            stroke: 'black',
            strokeWidth: 1,
      
        });
        layer.add(image);
        image.position(stage.getPointerPosition());

        const tr = new Konva.Transformer({
            nodes: [image],
            keepRatio: true,
            boundBoxFunc: (oldBox, newBox) => {
                if (newBox.width < 10 || newBox.height < 10) {
                    return oldBox;
                }
                return newBox;
            },
        });

        layer.add(tr);


        image.on('transform', () => {
            // reset scale on transform
            image.setAttrs({
                scaleX: 1,
                scaleY: 1,
                width: image.width() * image.scaleX(),
                height: image.height() * image.scaleY(),
            });
            applyCrop(image.getAttr('lastCropUsed'));
        });

        //image.on('mouseleave', function (evt) {
        //    tr.nodes([]);
           
        //});

        image.on('mouseenter', function (evt) {
            tr.nodes([image]);
            //image.setAttrs({
            //    fill: 'blue',
            //    stroke: 'blue',
            //    strokeWidth: 3,
            //});

            //var json = stage.toJSON();
            //console.log(json);
        });

        image.on('dblclick', function (evt) {
            evt.target.remove();
            tr.nodes([]);
            //image.setAttrs({
            //    fill: 'black',
            //    stroke: 'black',
            //    strokeWidth: 1,
            //});
        });

        stage.addEventListener('click', () => {
            tr.nodes([]);
        })

        //getKonvaCanvas[0].addEventListener('click', () => {
        //    tr.nodes([]);
        //})

    });
   

   
   /* document
        .getElementById('delete')
        .addEventListener('click', function (e) {
            layer.remove();
        });*/
   /* layer.on('dblclick', function (e) {
        console.log(e);
        const { target } = e;
        target.remove();
        target.Transformer.remove();
        
    });
*/
   
});

const SelectedBg = document.getElementById('bg_image');
const getCanvasContainer = document.getElementById('container');

SelectedBg.addEventListener('change', ({ target }) => {
    if (target.files[0]) {
        const srcOfImage = URL.createObjectURL(target.files[0]);
        getCanvasContainer.style.backgroundImage = `url(${srcOfImage})`;
        getCanvasContainer.onload = function () {
            URL.revokeObjectURL(getCanvasContainer.src) // free memory
        }
    };
});

/*stage.getContainer().style.backgroundImage = url('https://upload.wikimedia.org/wikipedia/commons/a/a8/TEIDE.JPG');*/
/*document.body.style.background = "url(" + canvas.toDataURL() + ")";*/

