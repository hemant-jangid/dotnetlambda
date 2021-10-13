FROM public.ecr.aws/lambda/dotnet:5.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /src
COPY ["DotnetLambda.csproj", "DotnetLambda/"]
RUN dotnet restore "DotnetLambda/DotnetLambda.csproj"

WORKDIR "/src/DotnetLambda"
COPY . .
RUN dotnet build "DotnetLambda.csproj" --configuration Release --output /app/build

FROM build AS publish
RUN dotnet publish "DotnetLambda.csproj" \
            --configuration Release \ 
            --runtime linux-x64 \
            --self-contained false \ 
            --output /app/publish \
            -p:PublishReadyToRun=true  

FROM base AS final
RUN yum install -y amazon-linux-extras
RUN amazon-linux-extras install epel -y
RUN yum install -y libgdiplus
WORKDIR /var/task
COPY --from=publish /app/publish .
CMD ["DotnetLambda::DotnetLambda.Function::FunctionHandler"]
