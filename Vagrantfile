Vagrant.configure("2") do |config|
  config.vm.box = "ubuntu/jammy64"
  config.vm.network "forwarded_port", guest: 8080, host: 8080
  config.vm.provider "virtualbox" do |vb|
    vb.memory = "2048"
    vb.cpus = 2
  end

  # Primero instala Puppet
  config.vm.provision "shell", inline: <<-SHELL
    apt-get update
    apt-get install -y puppet
  SHELL

  # Puppet instala Docker
  config.vm.provision "puppet" do |puppet|
    puppet.manifests_path = "puppet/manifests"
    puppet.manifest_file  = "default.pp"
  end

  # Levanta los contenedores después de Puppet
  config.vm.provision "shell", inline: <<-SHELL
    cd /vagrant
    sudo docker compose up -d
  SHELL
end