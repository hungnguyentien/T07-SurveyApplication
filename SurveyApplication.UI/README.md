# T07-SurveyApplication

import Plotly from "plotly.js-dist-min";

--- Run docker
docker build -t survey-ui:v1.0.0 -f ./Dockerfile .
docker run -p 8001:80 -d survey-ui:v1.0.0