# import flask microframework library
from flask import Flask
 
# initialize the flask application
app = Flask(__name__)
@app.route('/')
def hello_world():
    return 'Hello, World!'
if __name__ == "__main__":
#     run flask application in debug mode
    app.run(debug=True)
    #set FLASK_ENV=developmentfla
    #set FLASK_APP=application.py