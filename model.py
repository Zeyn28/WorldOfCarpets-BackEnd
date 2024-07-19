import sys
from numpy import empty
from ultralytics import YOLO


model_path="best.pt"

def getBBCoords(result):
    return result.boxes[0].cpu().numpy()


def getCarpetDimesions(cmPerPixel,coords):
    dimensions=type('obj', (object,), {})
    dimensions.width=coords[2]-coords[0]*cmPerPixel
    dimensions.height=coords[3]-coords[1]*cmPerPixel
    return dimensions



def YOLODetect(images):
    model=YOLO(model_path)
    results = model.predict(sys.argv[1],save=True)
    
    if(len(results[0].boxes)==0):
        return "No object detected"
    
    else:
        for result in results:
            boxes = result.boxes
        bbox = boxes.xyxy.tolist()[0]
        #print(bbox)
        
        coords=getBBCoords(result)
        coords=coords.xyxy[0].astype(int)
        dimesions=getCarpetDimesions(cmPerPixel=sys.argv[1],coords=coords)
        
        #print("Carpet dimesions are: ",dimesions)