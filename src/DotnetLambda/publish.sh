#!/bin/bash
docker build -t dotnetlambda .
docker tag dotnetlambda:latest 403343653796.dkr.ecr.ap-south-1.amazonaws.com/dotnetlambda:latest
docker push 403343653796.dkr.ecr.ap-south-1.amazonaws.com/dotnetlambda:latest
