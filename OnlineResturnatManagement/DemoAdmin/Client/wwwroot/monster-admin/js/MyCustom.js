var width = window.innerWidth;
var height = window.innerHeight;

var stage = new Konva.Stage({
    container: 'container',
    width: width,
    height: height,
});

var layer = new Konva.Layer({
    width: 150,
    height: 90,
    draggable: true,
});

var imageObj = new Image();
imageObj.src = 'https://cdn3.vectorstock.com/i/1000x1000/12/57/a-simple-nature-scene-vector-23891257.jpg';

imageObj.onload = function () {
    var map = new Konva.Image({
        x: 0,
        y: 0,
        image: imageObj,
        width: width,
        height: height
    });
    layer.add(map);
}




var text = new Konva.Text({
    x: 10,
    y: 15,
    text: 'Simple Text',
    fontSize: 30,
    fontFamily: 'Calibri',
    fill: 'green'
});

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

//For Text
var textNode = new Konva.Text({
    text: 'Some text here',
    x: 50,
    y: 50,
    fontSize: 20,
});
/*const getKonvaCanvas = document.getElementsByClassName('konvajs-content');*/
const getDeleteId = document.getElementById('delete-shape');
getDeleteId.addEventListener('click', () => {

})

//Image DragDrop
con.addEventListener('drop', function (e) {
    e.preventDefault();
    stage.setPointersPositions(e);

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

        image.on('click', function (evt) {
            tr.nodes([]);
            var json = stage.toJSON();
            console.log(json);
        });

        image.on('dblclick', function (evt) {
            tr.nodes([image]);
        });

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

    /* document
        .getElementById('save')
        .addEventListener('click', function (e) {
            state.forEach((item, index) => {
                console.log(item);
                console.log("index",index);

                *//*var node = new Konva.Image({
                    draggable: true,
                    name: 'item-' + index,
                    // make it smaller
                    scaleX: 0.5,
                    scaleY: 0.5,
                });*//*
            });
        });*/
   
});

