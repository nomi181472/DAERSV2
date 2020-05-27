# import flask microframework library
from flask import Flask, request, Response,jsonify
import sys

# initialize the flask application
app = Flask(__name__)
 
# endpoint api_1() with post method
@app.route("/api_1", methods=["POST"])
def api_1():
    return 'Api_1'
@app.route('/')
def hello_world():
    return 'Hello, World!'
@app.route("/api/v1.0/csharp_python_restfulapi_json", methods=["POST"])
def csharp_python_restfulapi_json():
    """
    simple c# test to call python restful api web service
    """
    try:                
#         get request json object
        request_json = request.get_json()      
#         convert to response json object 
        response = jsonify(request_json)
        response.status_code = 200  
    except:
        exception_message = sys.exc_info()[1]
        response = json.dumps({"content":exception_message})
        response.status_code = 400
    return response
if __name__ == "__main__":
#     run flask application in debug mode   
    app.debug = True
    app.run(host = '0.0.0.0',port=5005)
    #app.run(debug=True)
    #set FLASK_ENV=developmentfla
    #set FLASK_APP=application.py


    