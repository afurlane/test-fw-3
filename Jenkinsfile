pipeline {
    agent { node { label 'Master' } }
    environment {
        BASE_INSTALL_DIR = '/usr/local/test-fw-3'
        BASE_SYMLINK_DIR = '/usr/local/test-fw-3/current'
    }
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        stage('Prepare') {
            steps {
                echo 'Prepare phase...'
                cmd_exec("dotnet restore test-fw-3.sln")
                echo 'Prepare phase done!'
            }
        }
        stage('SonarScannerStart') {
            steps {
                withSonarQubeEnv('SonarQube Home') {
                    echo 'Set begin of scanning for Sonar'
                    cmd_exec("dotnet sonarscanner begin /s:$WORKSPACE/SonarQube.Analysis.xml /n:test-fw-3 /k:TESTFW3 /v:1.0 /d:sonar.host.url=${SONAR_HOST_URL} /d:sonar.login=${SONAR_AUTH_TOKEN}")
                }
            }
        }
        stage('Build') {
            steps {
                echo 'Build phase...'
                cmd_exec("dotnet build test-fw-3.sln -c Release")
                echo 'Build phase done!'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing phase...'
                cmd_exec("dotnet test test-fw-3.sln /p:CollectCoverage=true /p:CoverletOutputFormat=opencover")
                echo 'Testing phase done!'
            }
        }
        stage('SonarScannerEnd') {
            steps {
                withSonarQubeEnv('SonarQube Home') {
                    echo 'Set end of scanning for Sonar'
                    cmd_exec("dotnet sonarscanner end /d:sonar.login=${SONAR_AUTH_TOKEN}")
                }
            }
        }
        stage('WaitForQualityGate') {
            steps {
                timeout(time: 1, unit: 'HOURS') {
                    waitForQualityGate abortPipeline: true
                }
            }
        }
        /*
        stage('Build docker image') {
            steps {
                def customImage = docker.build("my-image:${env.BUILD_ID}")
                customImage.push()
            }
        }
        */
        stage('Publish release webapp for staging') {
            steps {
                cmd_exec("dotnet publish -c Release --self-contained=true --runtime linux-x64 --version-suffix ${env.BUILD_ID} --output ${env.WORKSPACE}/deploy.${env.BUILD_ID} WebApp")
            }
        }
        stage('SSH prepare & publish') {
            steps {
                script {
                    def remote = [:]
                    remote.name = "node"
                    remote.host = "home.illogi.co"
                    remote.allowAnyHosts = true
                    remote.retryCount = 3
                    remote.retryWaitSec = 10
                    remote.fileTransfer = "scp"
                    remote.logLevel = "FINEST"
                    remote.pty = true
 
                    stage('SSH publish WebApp') {
                        withCredentials([usernamePassword(credentialsId: '4fcd00fe-b57e-4537-9995-40b097e395ca', passwordVariable: 'password', usernameVariable: 'userName')]) {
                            remote.user = userName
                            remote.password = password
                            sshCommand remote: remote, sudo: true, command: "mkdir ${env.BASE_INSTALL_DIR}", failOnError: false
                            sshCommand remote: remote, sudo: true, command: "chmod 775 ${env.BASE_INSTALL_DIR}"
                            sshCommand remote: remote, sudo: true, command: "chown ${remote.user} ${env.BASE_INSTALL_DIR}"
                            sshCommand remote: remote, sudo: true, command: "mkdir ${env.BASE_INSTALL_DIR}/deploy.${env.BUILD_ID}", failOnError: false
                            /* sshCommand remote: remote, sudo: true, command: "mkdir ${env.BASE_INSTALL_DIR}/deploy.${env.BUILD_ID}/Modies API", failOnError: false */
                            sshCommand remote: remote, sudo: true, command: "chown -R ${remote.user} ${env.BASE_INSTALL_DIR}/deploy.${env.BUILD_ID}"
                            sshCommand remote: remote, sudo: true, command: "chmod 777 ${env.BASE_INSTALL_DIR}"
                            sshPut remote: remote, from: "${env.WORKSPACE}/deploy.${env.BUILD_ID}/WebApp", into: "${env.BASE_INSTALL_DIR}/deploy.${env.BUILD_ID}"
                            sshCommand remote: remote, sudo: true, command: "chmod 777 ${env.BASE_INSTALL_DIR}/testnetcore.service", failOnError: false
                            sshPut remote: remote, from: "${env.WORKSPACE}/testnetcore.service", into: "${env.BASE_INSTALL_DIR}"
                            sshCommand remote: remote, sudo: true, command: "chmod 775 ${env.BASE_INSTALL_DIR}"
                            sshCommand remote: remote, sudo: true, command: "mv ${env.WORKSPACE}/testnetcore.service /etc/systemd/system"
                            sshCommand remote: remote, sudo: true, command: "find ${env.BASE_INSTALL_DIR}/deploy.${env.BUILD_ID} -type d -exec chmod 775 {} +"
                            sshCommand remote: remote, sudo: true, command: "find ${env.BASE_INSTALL_DIR}/deploy.${env.BUILD_ID} -type f -exec chmod 440 {} +"
                            sshCommand remote: remote, sudo: true, command: "rm ${env.BASE_INSTALL_DIR}/current"
                            sshCommand remote: remote, sudo: true, command: "ln -s ${env.BASE_INSTALL_DIR}/deploy.${env.BUILD_ID}/WebApp ${env.BASE_INSTALL_DIR}/current"
                            sshCommand remote: remote, sudo: true, command: "systemctl daemon-reload"
                            sshCommand remote: remote, sudo: true, command: "systemctl enable testnetcore"
                            sshCommand remote: remote, sudo: true, command: "systemctl restart testnetcore"
                        }
                    }
                }
            }
        }
    }
    post { 
        always { 
            echo 'Remember, if dockerizing, to cleanup old images like in the buildDiscarder'
        }
    }
    options {
        buildDiscarder(logRotator(numToKeepStr: '10', artifactNumToKeepStr: '10'))
    }
}
 
def cmd_exec(command) {
    if (isUnix()) {
        $BAT_STATUS = sh(returnStdout: true, script: "${command}").trim()
    } else {
        $BAT_STATUS = bat(returnStdout: true, script: "@${command}").trim()
    }
    echo $BAT_STATUS
    return $BAT_STATUS
}

v1.9.1
